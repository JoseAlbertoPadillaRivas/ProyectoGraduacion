using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class HistorialController : Controller
    {
        // GET: Historial
        public ActionResult IndexHistorial()
        {
            return View();
        }

        // GET: VISTA PROVISIONAL
        public ActionResult IndexHistorialAdmin()
        {
            return View();
        }

        // GET: Historial/RegistrarHistoria
        public ActionResult RegistrarHistoria()
        {
            return View();
        }

        // GET: Historial/ModificarHistoria
        public ActionResult ModificarHistoria()
        {
            return View();
        }

        // GET: Historial/AñadirDocumentos
        public ActionResult AñadirDocumentos()
        {
            return View();
        }

        // GET: Historial/VerHistoria
        public ActionResult VerHistoria()
        {
            return View();
        }

        // GET: Historial/DescargarDocumentos
        public ActionResult DescargarDocumentos()
        {
            return View();
        }
    }
}
