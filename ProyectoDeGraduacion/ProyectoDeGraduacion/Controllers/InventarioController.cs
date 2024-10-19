using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
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

        //[HttpPost]
        //public ActionResult EliminarProducto(Inventario inventario)
        //{
        //    var respuesta = inventarioM.EliminarProducto(inventario);

        //    if (respuesta)
        //        return RedirectToAction("MostrarInventario", "Inventario");
        //    else
        //    {
        //        ViewBag.msj = "Error al eliminar";
        //        return View();
        //    }
        //}

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

    }
}