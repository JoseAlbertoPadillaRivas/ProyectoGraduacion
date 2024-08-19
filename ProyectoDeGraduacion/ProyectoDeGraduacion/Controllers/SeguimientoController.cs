using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class SeguimientoController : Controller
    {
        [HttpGet]
        public ActionResult MostrarProductos()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ActualizarProducto()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MisProductos()
        {
            return View();
        }

    }
}