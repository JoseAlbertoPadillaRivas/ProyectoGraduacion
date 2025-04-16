
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

        [ValidarSesionUnicaAttribute]
        [HttpPost]
        public ActionResult Login(Pacientes paciente)
        {
            var respuesta = pacienteM.IniciarSesion(paciente);
            if (respuesta != null)
            {
                // Verificar si ya hay una sesión activa para este usuario
                if (!pacienteM.IsUserSessionAvailable(respuesta.idPaciente))
                {
                    ViewBag.msj = "Esta cuenta ya tiene una sesión activa. Cierre la sesión previa para continuar.";
                    return View();
                }

                // Generar un token único para la sesión
                string sessionToken = System.Guid.NewGuid().ToString();
                Session["NombreUsuario"] = respuesta.Nombre;
                Session["idUsuario"] = respuesta.idPaciente;
                Session["RolUsuario"] = respuesta.IdRol.ToString();
                Session["SessionToken"] = sessionToken;

                // Actualizar el token en la base de datos
                pacienteM.UpdateUserSessionToken(respuesta.idPaciente, sessionToken);

                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.msj = "Su información no es correcta";
                return View();
            }
        }

        [ValidarSesionUnicaAttribute]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            if (Session["idUsuario"] != null)
            {
                int idUsuario = (int)Session["idUsuario"];
                pacienteM.UpdateUserSessionToken(idUsuario, null); // Limpia token y expiración
            }
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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


    }
}