using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class PerfilController : Controller
    {
        PacientesModel pacienteM = new PacientesModel();

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult MiPerfil()
        {
            var respuesta = pacienteM.ConsultarUsuarioidPaciente(int.Parse(Session["idUsuario"].ToString()));
            return View(respuesta);
        }

        [FiltroSeguridad]
        [HttpPost]
        public ActionResult ActualizarPaciente(Pacientes paciente)
        {
            var respuesta = pacienteM.ActualizarPaciente(paciente);

            if (respuesta)
                return RedirectToAction("MiPerfil", "Perfil");
            else
            {
                ViewBag.msj = "No se ha podido actualizar su información de perfil";
                return View();
            }
        }
    }
}