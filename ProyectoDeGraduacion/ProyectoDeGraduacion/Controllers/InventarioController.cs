using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [FiltroAdmin]
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class InventarioController : Controller
    {
        InventarioModel inventarioM = new InventarioModel();
        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();

        [HttpGet]
        public ActionResult MostrarInventario()
        {
            var respuesta = inventarioM.ConsultarInventario();
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult EliminarProducto(int idProducto)
        {
            // Verificar relación con órdenes en estado "Pendiente"
            var relacionadoConPendiente = inventarioM.VerificarRelacionOrden(idProducto);

            if (relacionadoConPendiente)
            {
                return Json(new { success = false, message = "No se puede eliminar el producto porque está asociado a una orden de compra en estado 'Pendiente'." });
            }

            // Intentar eliminar el producto
            var eliminado = inventarioM.EliminarProducto(idProducto);
            if (eliminado)
            {
                return Json(new { success = true, message = "Producto eliminado correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo eliminar el producto. Intenta nuevamente." });
            }
        }


        [HttpGet]
        public ActionResult ActualizarProducto(int idProducto)
        {
            var proveedores = _context.tProveedores.ToList();
            ViewBag.Proveedores = new SelectList(proveedores, "idProveedor", "Empresa");
            var respuesta = inventarioM.ConsultarProductoID(idProducto);
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult ActualizarProducto(Inventario inventario)
        {
            var respuesta = inventarioM.ActualizarProducto(inventario);

            if (respuesta)
                return RedirectToAction("MostrarInventario", "Inventario");
            else
            {
                ViewBag.msj = "Error al actualizar";
                return View();
            }
        }

        [HttpGet]
        public ActionResult AgregarProducto()
        {
            var proveedores = _context.tProveedores.ToList();
            ViewBag.Proveedores = new SelectList(proveedores, "idProveedor", "Empresa");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(Inventario inventario)
        {
            var respuesta = inventarioM.RegistrarInventario(inventario);

            if (respuesta)
                return RedirectToAction("MostrarInventario", "Inventario");
            else
            {
                ViewBag.msj = "Error al registrar informacion";
                return View();
            }
        }

        [HttpGet]
        public ActionResult MostrarProveedores()
        {
            var respuesta = inventarioM.ConsultarProveedores();
            return View(respuesta);
        }

        [HttpPost]
        public JsonResult RegistrarProveedor(Proveedor proveedor)
        {

            try
            {
                // Lógica para registrar el proveedor
                var resultado = inventarioM.RegistrarProveedor(proveedor);

                if (resultado)
                {
                    return Json(new { success = true, message = "Proveedor registrado correctamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Error al registrar el proveedor. Verifica los datos." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        [HttpPost]
        public ActionResult ActualizarProveedor(Proveedor proveedor)
        {
            try
            {
                var respuesta = inventarioM.ActualizarProveedor(proveedor);

                if (respuesta)
                {
                    TempData["SuccessMessage"] = "Proveedor actualizado correctamente.";
                    return RedirectToAction("MostrarProveedores", "Inventario");
                }
                else
                {
                    ViewBag.msj = "No se pudo actualizar el proveedor. Inténtelo nuevamente.";
                    return View(proveedor); // Devuelve la vista con los datos ingresados
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción y mostrar un mensaje amigable al usuario
                ViewBag.msj = "Ocurrió un error al actualizar el proveedor: " + ex.Message;
                return View(proveedor); // Devuelve la vista con los datos ingresados
            }
        }

        [HttpPost]
        public ActionResult CambiarEstadoProveedor(Proveedor proveedor)
        {
            try
            {
                var respuesta = inventarioM.CambiarEstadoProveedor(proveedor);

                if (respuesta)
                {
                    TempData["SuccessMessage"] = "El estado del proveedor se ha cambiado correctamente.";
                    return RedirectToAction("MostrarProveedores", "Inventario");
                }
                else
                {
                    ViewBag.msj = "No se pudo cambiar el estado del proveedor. Inténtelo nuevamente.";
                    return View(proveedor); // Devuelve la vista con los datos ingresados
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción y mostrar un mensaje amigable al usuario
                ViewBag.msj = "Ocurrió un error al intentar cambiar el estado del proveedor: " + ex.Message;
                return View(proveedor); // Devuelve la vista con los datos ingresados
            }
        }


    }
}