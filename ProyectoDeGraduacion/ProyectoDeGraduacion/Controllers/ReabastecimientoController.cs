using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using ProyectoGraduacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rotativa;
using System.Text;
namespace ProyectoDeGraduacion.Controllers
{
    public class ReabastecimientoController : Controller
    {
        private ProyectoGraduacionEntities db = new ProyectoGraduacionEntities(); // Contexto de la base de datos
       ReabastecimientoModel reabastecimientoM = new ReabastecimientoModel();

        // Vista principal del módulo de Reabastecimiento
        public ActionResult IndexReabastecimiento()
        {
            return View();
        }

        // Vista para Configurar Valores de Reabastecimiento
        public ActionResult ConfigurarValores()
        {
            InventarioModel inventarioModel = new InventarioModel();
            var productos = inventarioModel.ConsultarInventario();
            return View(productos);
        }

        // Método POST para actualizar los valores mínimos de reabastecimiento
        [HttpPost]
        public ActionResult ConfigurarValores(List<tInventario> updatedProducts)
        {
            foreach (var updatedProduct in updatedProducts)
            {
                var product = db.tInventario.FirstOrDefault(p => p.idProducto == updatedProduct.idProducto);
                if (product != null)
                {
                    product.NivelMinimoStock = updatedProduct.NivelMinimoStock; // Actualizamos nivel mínimo de stock
                }
            }

            db.SaveChanges(); // Guardamos los cambios en la base de datos
            TempData["SuccessMessage"] = "Los valores de reabastecimiento se han configurado correctamente.";
            return RedirectToAction("ConfigurarValores");
        }


        // Vista para Generar Órdenes de Compra
        public ActionResult GenerarOrdenes()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                // Obtener productos y proveedores
                var productos = context.tInventario.ToList();
                var proveedores = context.tProveedores.ToList();

                // Pasar los productos y proveedores a la vista usando ViewBag
                ViewBag.Proveedores = proveedores;
                return View(productos);
            }
        }


        [HttpPost]
        public ActionResult GenerarOrdenes(FormCollection form)
        {
            bool ordenGenerada = false;
            int idOrdenCompra = 0;

            StringBuilder productosHtml = new StringBuilder();

            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("cantidad_"))
                {
                    var idProductoStr = key.Replace("cantidad_", "");
                    int idProducto = int.Parse(idProductoStr);
                    var cantidadString = form[key];
                    if (string.IsNullOrEmpty(cantidadString) || cantidadString == "0")
                    {
                        continue;
                    }
                    int cantidadSolicitada = int.Parse(cantidadString);
                    var proveedorString = form["proveedor_" + idProductoStr];
                    if (string.IsNullOrEmpty(proveedorString))
                    {
                        continue;
                    }
                    int idProveedor = int.Parse(proveedorString);

                    if (!ordenGenerada)
                    {
                        tOrdenesCompra nuevaOrdenCompra = new tOrdenesCompra
                        {
                            idProveedor = idProveedor,
                            FechaSolicitud = DateTime.Now,
                            EstadoOrden = "Pendiente"
                        };

                        db.tOrdenesCompra.Add(nuevaOrdenCompra);
                        db.SaveChanges();
                        ordenGenerada = true;
                        idOrdenCompra = nuevaOrdenCompra.idOrdenCompra;
                    }

                    var productoInfo = db.tInventario.FirstOrDefault(p => p.idProducto == idProducto);
                    productosHtml.Append($"<tr><td>{productoInfo.NombreProducto}</td><td>{cantidadSolicitada}</td></tr>");
                }
            }

            if (ordenGenerada)
            {
                var ordenCompra = db.tOrdenesCompra.FirstOrDefault(o => o.idOrdenCompra == idOrdenCompra);
                var proveedor = db.tProveedores.FirstOrDefault(p => p.idProveedor == ordenCompra.idProveedor);

                if (proveedor != null)
                {
                    string correoProveedor = proveedor.Correo;

                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "correoProveedor.html";
                    string contenido = System.IO.File.ReadAllText(ruta);

                    contenido = contenido.Replace("@@NombreProveedor", proveedor.Empresa);
                    contenido = contenido.Replace("@@Productos", productosHtml.ToString());
                    contenido = contenido.Replace("@@Fecha", DateTime.Now.ToString("dd/MM/yyyy"));

                    GeneralModel generalM = new GeneralModel();
                    generalM.EnviarCorreo(correoProveedor, "Orden de Compra - Reabastecimiento", contenido);

                    TempData["SuccessMessage"] = "La orden de compra fue generada y enviada correctamente al proveedor.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "No se seleccionaron productos para generar órdenes.";
            }

            return RedirectToAction("GenerarOrdenes");
        }


        // Vista para Confirmar Recepción de Productos
        public ActionResult ConfirmarRecepcion()
        {
            // Obtener las órdenes de compra pendientes
            var ordenesPendientes = db.tOrdenesCompra
                                      .Where(o => o.EstadoOrden == "Pendiente")
                                      .ToList();

            // Crear una lista para pasar los productos de estas órdenes
            var productosPendientes = new List<tOrdenesProductos>();

            foreach (var orden in ordenesPendientes)
            {
                // Obtener los productos asociados a la orden de compra
                var productosOrden = db.tOrdenesProductos
                                       .Where(op => op.idOrdenCompra == orden.idOrdenCompra)
                                       .ToList();

                // Agregar los productos a la lista
                productosPendientes.AddRange(productosOrden);
            }

            // Pasar los productos pendientes a la vista
            return View(productosPendientes);
        }

        [HttpPost]
        public ActionResult ConfirmarRecepcion(FormCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("cantidad_"))
                {
                    // Extraer el idProducto desde el nombre del campo
                    int idProducto = int.Parse(key.Split('_')[1]);

                    // Obtener la cantidad recibida
                    int cantidadRecibida = int.Parse(form[key]);

                    // Buscar el producto en la base de datos
                    var producto = db.tInventario.FirstOrDefault(p => p.idProducto == idProducto);
                    if (producto != null)
                    {
                        // Actualizar la cantidad en el inventario
                        producto.Cantidad += cantidadRecibida;

                        // Actualizar el estado de la orden de compra como completada
                        var ordenProducto = db.tOrdenesProductos.FirstOrDefault(op => op.idProducto == idProducto);
                        if (ordenProducto != null)
                        {
                            var orden = db.tOrdenesCompra.FirstOrDefault(o => o.idOrdenCompra == ordenProducto.idOrdenCompra);
                            orden.EstadoOrden = "Completada";
                        }

                        // Guardar los cambios en la base de datos
                        db.SaveChanges();
                    }
                }
            }

            // Mostrar ventana emergente de confirmación
            TempData["mensaje"] = "La recepción de productos se ha registrado correctamente y el inventario ha sido actualizado.";
            return RedirectToAction("ConfirmarRecepcion");
        }


        // Vista para el Historial de Reabastecimientos
        [HttpGet]
        public ActionResult HistorialReabastecimiento()
        {
            var respuesta = reabastecimientoM.ConsultarCompras();
            return View(respuesta);
        }

        // Vista específica para PDF
        public ActionResult HistorialReabastecimientoPDF()
        {
            var respuesta = reabastecimientoM.ConsultarCompras();
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult GenerarPdf()
        {
            var respuesta = reabastecimientoM.ConsultarCompras();

            var pdfResult = new ActionAsPdf("HistorialReabastecimientoPDF", respuesta)
            {
                FileName = "Historial.pdf"
            };

            return pdfResult;
        }


    }
}