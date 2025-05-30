﻿using ProyectoDeGraduacion.BaseDatos;
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
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class ReabastecimientoController : Controller
    {
        private ProyectoGraduacionEntities db = new ProyectoGraduacionEntities();

        public ActionResult IndexReabastecimiento()
        {
            return View();
        }

        public ActionResult ConfigurarValores()
        {
            InventarioModel inventarioModel = new InventarioModel();
            var productos = inventarioModel.ConsultarInventario();
            return View(productos);
        }

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
                                            idOrdenCompra = o.idOrdenCompra, // Usar IdOrdenCompra
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
            try
            {
                // Buscar la orden en la base de datos
                var orden = db.tOrdenesCompra.FirstOrDefault(o => o.idOrdenCompra == idOrdenCompra);

                if (orden == null)
                {
                    // Si no se encuentra la orden, devolver un error
                    return Json(new { success = false, message = "No se encontró la orden de compra." });
                }

                // Verificar el estado actual de la orden
                if (orden.EstadoOrden == "Pendiente")
                {
                    // Cambiar el estado a "Terminado"
                    orden.EstadoOrden = "Terminado";
                    db.SaveChanges();

                    // Devolver éxito junto con el ID de la orden
                    return Json(new { success = true, message = "Recepción confirmada correctamente.", idOrdenCompra = idOrdenCompra });
                }

                if (orden.EstadoOrden == "Terminado")
                {
                    // Si ya está terminado, devolver un mensaje informativo
                    return Json(new { success = false, message = "La orden ya fue marcada como 'Terminado' previamente." });
                }

                // Manejar otros estados si fuera necesario
                return Json(new { success = false, message = $"Estado inválido: {orden.EstadoOrden}." });
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados
                return Json(new { success = false, message = $"Error inesperado: {ex.Message}" });
            }
        }




        [HttpPost]
        public ActionResult RegistrarDiscrepancia(int idOrdenCompra, string mensajeDiscrepancia)
        {
            try
            {
                var ordenCompra = db.tOrdenesCompra.FirstOrDefault(o => o.idOrdenCompra == idOrdenCompra);

                if (ordenCompra == null)
                {
                    TempData["ErrorMessage"] = "No se encontró la orden de compra.";
                    return RedirectToAction("ConfirmarRecepcion");
                }

                var proveedor = db.tProveedores.SingleOrDefault(p => p.idProveedor == ordenCompra.idProveedor);


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


        public ActionResult HistorialReabastecimiento()
        {
            var historialOrdenes = (from orden in db.tOrdenesCompra
                                    join proveedor in db.tProveedores
                                    on orden.idProveedor equals proveedor.idProveedor
                                    select new HistorialORD
                                    {
                                        NombreProducto = orden.NombreProducto,
                                        CantidadTotalSolicitada = orden.CantidadTotalSolicitada,
                                        FechaSolicitud = orden.FechaSolicitud,
                                        NombreProveedor = proveedor.Empresa,
                                        idOrdenCompra = orden.idOrdenCompra
                                    }).ToList();

            return View(historialOrdenes);
        }

        [AllowAnonymous]
        public ActionResult HistorialReabastecimientoPDF()
        {
            var historialOrdenes = (from orden in db.tOrdenesCompra
                                    join proveedor in db.tProveedores
                                    on orden.idProveedor equals proveedor.idProveedor
                                    select new HistorialORD
                                    {
                                        NombreProducto = orden.NombreProducto,
                                        CantidadTotalSolicitada = orden.CantidadTotalSolicitada,
                                        FechaSolicitud = orden.FechaSolicitud,
                                        NombreProveedor = proveedor.Empresa,
                                        idOrdenCompra = orden.idOrdenCompra
                                    }).ToList();

            return View(historialOrdenes);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GenerarPdf()
        {
            var pdfResult = new ActionAsPdf("HistorialReabastecimientoPDF")
            {
                FileName = "Historial.pdf"
            };

            var pdfBytes = pdfResult.BuildFile(this.ControllerContext);

            // (Opcional) Eliminar los registros después de generar el PDF
            db.tOrdenesCompra.RemoveRange(db.tOrdenesCompra);
            db.SaveChanges();

            return File(pdfBytes, "application/pdf", "Historial.pdf");
        }




        [HttpPost]
        public ActionResult EliminarOrdenCompra(int idOrdenCompra)
        {
            ReabastecimientoModel reabastecimientoModel = new ReabastecimientoModel();
            var orden = reabastecimientoModel.EliminarOrdenCompre(idOrdenCompra);

            if (orden)
            {
                return Json(new { success = true, message = "Producto eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo eliminar el producto. Intenta nuevamente." });
            }
        }



    }
}