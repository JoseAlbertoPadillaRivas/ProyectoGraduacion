
using System;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class LoginController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
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


        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }


    }
}