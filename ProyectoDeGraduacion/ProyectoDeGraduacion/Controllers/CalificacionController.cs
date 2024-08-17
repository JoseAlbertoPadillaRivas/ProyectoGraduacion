using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class CalificacionController : Controller
    {
        [HttpGet]
        public ActionResult verCalificaciones()
        {
            return View();
        }

        public ActionResult NuevaCalificacion()
        {
            return View();
        }

        public ActionResult AdminCalificacion()
        {
            return View();
        }


    }
}