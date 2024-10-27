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
                    product.NivelMinimoStock = updatedProduct.NivelMinimoStock; 
                }
            }

            db.SaveChanges(); 
            TempData["SuccessMessage"] = "Los valores de reabastecimiento se han configurado correctamente.";
            return RedirectToAction("ConfigurarValores");
        }


        // Vista para Generar Órdenes de Compra
        public ActionResult GenerarOrdenes()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var productos = context.tInventario.ToList();
                var proveedores = context.tProveedores.ToList();

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
                        var productoInfo = db.tInventario.FirstOrDefault(p => p.idProducto == idProducto);

                        tOrdenesCompra nuevaOrdenCompra = new tOrdenesCompra
                        {
                            idProveedor = idProveedor,
                            FechaSolicitud = DateTime.Now,
                            EstadoOrden = "Pendiente",
                            NombreProducto = productoInfo.NombreProducto,
                            CantidadTotalSolicitada = cantidadSolicitada,
                            idProducto = idProducto
                        };

                        db.tOrdenesCompra.Add(nuevaOrdenCompra);
                        db.SaveChanges();

                        ordenGenerada = true;
                        idOrdenCompra = nuevaOrdenCompra.idOrdenCompra;

                        productosHtml.Append($"<tr><td>{productoInfo.NombreProducto}</td><td>{cantidadSolicitada}</td></tr>");
                    }
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

        public ActionResult ConfirmarRecepcion()
        {
            var productosPendientes = db.tOrdenesCompra
                                        .Where(o => o.EstadoOrden == "Pendiente")
                                        .Select(o => new ProductoPendiente
                                        {
                                            IdProducto = o.idProducto,
                                            NombreProducto = o.NombreProducto,
                                            CantidadSolicitada = o.CantidadTotalSolicitada,
                                            Proveedor = o.tProveedores.Empresa,
                                            FechaSolicitud = o.FechaSolicitud
                                        })
                                        .ToList();

            return View(productosPendientes);
        }

        [HttpPost]
        public JsonResult ActualizarEstadoOrden(int idOrdenCompra)
        {
            var orden = db.tOrdenesCompra.FirstOrDefault(o => o.idOrdenCompra == idOrdenCompra);

            if (orden != null)
            {
                orden.EstadoOrden = "Terminado";
                db.SaveChanges();
                return Json(new { success = true, message = "Recepción confirmada." });
            }
            else
            {
                return Json(new { success = false, message = "No se encontró la orden de compra." });
            }
        }


        [HttpPost]
        public ActionResult RegistrarDiscrepancia(int idProducto, string mensajeDiscrepancia)
        {
            try
            {
                var ordenCompra = db.tOrdenesCompra.FirstOrDefault(o => o.idProducto == idProducto && o.EstadoOrden == "Pendiente");

                if (ordenCompra == null)
                {
                    TempData["ErrorMessage"] = "No se encontró la orden de compra.";
                    return RedirectToAction("ConfirmarRecepcion");
                }

                var proveedor = db.tProveedores.FirstOrDefault(p => p.idProveedor == ordenCompra.idProveedor);

                if (proveedor == null)
                {
                    TempData["ErrorMessage"] = "No se pudo obtener la información del proveedor.";
                    return RedirectToAction("ConfirmarRecepcion");
                }

                string ruta = AppDomain.CurrentDomain.BaseDirectory + "correoDiscrepancia.html";
                string contenido = System.IO.File.ReadAllText(ruta);

                contenido = contenido.Replace("@@NombreProveedor", proveedor.Empresa);
                contenido = contenido.Replace("@@Discrepancias", $"<tr><td>{ordenCompra.NombreProducto}</td><td>{ordenCompra.CantidadTotalSolicitada}</td></tr>");
                contenido = contenido.Replace("@@Mensaje", mensajeDiscrepancia);

                GeneralModel generalM = new GeneralModel();
                generalM.EnviarCorreo(proveedor.Correo, "Discrepancia en Recepción - Clínica Dental Dra. Mariana Garro", contenido);

                TempData["SuccessMessage"] = "La discrepancia fue registrada y se envió una notificación al proveedor.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al registrar la discrepancia: {ex.Message}";
            }

            return RedirectToAction("ConfirmarRecepcion");
        }

        // Vista para el Historial de Reabastecimientos
        public ActionResult HistorialReabastecimiento()
        {
            var historialOrdenes = db.tOrdenesCompra
                                     .Select(o => new HistorialOrdenes
                                     {
                                         NombreProducto = o.NombreProducto,
                                         Cantidad = o.CantidadTotalSolicitada,
                                         FechaCompra = o.FechaSolicitud,
                                         Proveedor = o.tProveedores.Empresa
                                     })
                                     .ToList();

            if (!historialOrdenes.Any())
            {
                ViewBag.msj = "No se encontraron órdenes de compra en el historial.";
            }

            return View(historialOrdenes);
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