using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [FiltroAdmin]
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class PacientesController : Controller
    {

        PacientesModel pacienteM = new PacientesModel();

        [HttpGet]
        public ActionResult ConsultarPacientes()
        {
            var respuesta = pacienteM.ConsultarPacientes();
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult CambiarEstadoPaciente(Pacientes paciente)
        {
            var respuesta = pacienteM.CambiarEstadoPaciente(paciente);

            if (respuesta)
                return RedirectToAction("ConsultarPacientes", "Pacientes");
            else
            {
                ViewBag.msj = "No se ha podido inactivar la información del usuario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarPaciente(int idPaciente)
        {
            var respuesta = pacienteM.ConsultarUsuarioidPaciente(idPaciente);
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult ActualizarPaciente(Pacientes paciente)
        {
            var respuesta = pacienteM.ActualizarPaciente(paciente);

            if (respuesta)
                return RedirectToAction("ConsultarPacientes", "Pacientes");
            else
            {
                ViewBag.msj = "No se ha podido actualizar su información de perfil";
                return View();
            }
        }
    }
}