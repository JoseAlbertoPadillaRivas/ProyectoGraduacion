using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class CitasController : Controller
    {

        CitasModel citasM = new CitasModel();
        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();

        [HttpPost]
        public JsonResult RegistrarCita(Citas cita)
        {
            try
            {
                if (cita == null || cita.idPaciente <= 0 || cita.idSede <= 0 || cita.idCitaDisponible <= 0)
                {
                    return Json(new { success = false, message = "Por favor, completa todos los campos requeridos." });
                }

                var respuesta = citasM.RegistrarCita(cita);
                if (respuesta)
                {
                    return Json(new { success = true, message = "Cita registrada exitosamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Error al registrar la cita. Inténtelo nuevamente." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocurrió un error: " + ex.Message });
            }
        }



        [HttpGet]
        public ActionResult MisCitas()
        {
            // Obtener el idPaciente de la sesión
            int idPaciente = (int)Session["idUsuario"];

            // Pasar el idPaciente como parte del modelo o ViewBag
            ViewBag.IdPaciente = idPaciente;

            // Llamar al método que obtiene las citas del usuario
            var respuesta = citasM.MisCitas(idPaciente);

            // Obtener la lista de sedes y citas disponibles
            var sedes = _context.tSede.ToList();
            ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");

            var citasDisponibles = _context.tCitasDisponibles
                .Where(c => c.Estado == true) // Filtrar citas disponibles
                .ToList();
            ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");

            // Pasar la lista de citas y los datos adicionales a la vista
            return View(respuesta);
        }



        [HttpGet]
        public ActionResult CitasProgramadas()
        {
            int idPaciente = (int)Session["idUsuario"];

            // Pasar el idPaciente como parte del modelo o ViewBag
            ViewBag.IdPaciente = idPaciente;

            var sedes = _context.tSede.ToList();
            ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");

            var citasDisponibles = _context.tCitasDisponibles
                .Where(c => c.Estado == true) // Filtrar citas disponibles
                .ToList();
            ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");

            var respuesta = citasM.CitasProgramadas();
            return View(respuesta);
        }

        [HttpPost]
        public JsonResult ReprogramarCita(Citas cita)
        {
            try
            {
                if (cita == null || cita.idCita <= 0 || cita.idPaciente <= 0 || cita.idSede <= 0 || cita.idCitaDisponible <= 0)
                {
                    return Json(new { success = false, message = "Por favor, completa todos los campos requeridos." });
                }

                var respuesta = citasM.ReprogramarCita(cita);

                if (respuesta)
                {
                    return Json(new { success = true, message = "Cita reprogramada exitosamente." });
                }
                else
                {
                    return Json(new { success = false, message = "No se pudo reprogramar la cita. Inténtalo nuevamente." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocurrió un error: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult CancelarCita(int idCita)
        {
            try
            {
                if (idCita <= 0)
                {
                    return Json(new { success = false, message = "ID de cita no válido. Por favor, verifica los datos." });
                }

                var respuesta = citasM.CancelarCita(idCita);

                if (respuesta)
                {
                    return Json(new { success = true, message = "Cita cancelada correctamente." });
                }
                else
                {
                    return Json(new { success = false, message = "No se pudo cancelar la cita. Inténtalo nuevamente." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocurrió un error: " + ex.Message });
            }
        }



    }
}