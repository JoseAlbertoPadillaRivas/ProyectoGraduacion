using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class CitasController : Controller
    {
        [HttpGet]
        public ActionResult RegistrarCita()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MisCitas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CitasProgramadas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReprogramarCita()
        {
            return View();
        }
    }
}