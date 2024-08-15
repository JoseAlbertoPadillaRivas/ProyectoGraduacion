using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        [HttpGet]
        public ActionResult MostrarInventario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ActualizarProducto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AgregarProducto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EliminarProducto()
        {
            return RedirectToAction("MostrarInventario", "Inventario");
        }
    }
}