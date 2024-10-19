
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class LoginController : Controller
    {
        PacientesModel pacienteM = new PacientesModel();


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Pacientes paciente)
        {
            var respuesta = pacienteM.IniciarSesion(paciente);

            if (respuesta != null)
            {

                Session["NombreUsuario"] = respuesta.Nombre;
                Session["idUsuario"] = respuesta.idPaciente;
                Session["RolUsuario"] = respuesta.IdRol.ToString();
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.msj = "Su información no es correcta";
                return View();
            }
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public ActionResult Registrarse()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registrarse(Pacientes paciente)
        {
            var respuesta = pacienteM.RegistrarPaciente(paciente);

            if (respuesta)
                return RedirectToAction("Login", "Login");
            else
            {
                ViewBag.msj = "Error al registrar informacion";
                return View();
            }
        }


        [FiltroSeguridad]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


    }
}