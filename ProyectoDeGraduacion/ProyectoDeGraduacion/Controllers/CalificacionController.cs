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
        [HttpGet]
        public ActionResult NuevaCalificacion()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminCalificacion()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AnalizarCalificaciones()
        {
            return View();
        }

    }
}