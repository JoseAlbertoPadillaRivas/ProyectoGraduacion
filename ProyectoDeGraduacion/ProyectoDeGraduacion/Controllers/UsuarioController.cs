using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MiPerfil()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ActualizarUsuario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}