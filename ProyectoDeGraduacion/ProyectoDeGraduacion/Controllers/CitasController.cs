using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            int idPaciente = (int)Session["idUsuario"];
            ViewBag.IdPaciente = idPaciente;

            var respuesta = citasM.MisCitas(idPaciente);
            var sedes = _context.tSede.ToList();
            ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");

            // Obtener la hora del servidor en UTC y convertirla a la zona local
            DateTime ahoraUtc = DateTime.UtcNow;
            TimeZoneInfo zonaLocal = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Ajusta esto según tu país
            DateTime ahoraLocal = TimeZoneInfo.ConvertTimeFromUtc(ahoraUtc, zonaLocal);

            // Filtrar citas futuras en la zona horaria correcta
            var citasDisponibles = _context.tCitasDisponibles
                .Where(c => c.Estado == true && c.Fecha > ahoraLocal)
                .OrderBy(c => c.Fecha)
                .ToList();

            ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");

            return View(respuesta);
        }

        [FiltroAdmin]
        [HttpGet]
        public ActionResult CitasProgramadas()
        {
            int idPaciente = (int)Session["idUsuario"];
            ViewBag.IdPaciente = idPaciente;

            var sedes = _context.tSede.ToList();
            ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");

            // Obtener la hora UTC y convertirla a la zona local
            DateTime ahoraUtc = DateTime.UtcNow;
            TimeZoneInfo zonaLocal = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Ajusta según tu país
            DateTime ahoraLocal = TimeZoneInfo.ConvertTimeFromUtc(ahoraUtc, zonaLocal);

            var citasDisponibles = _context.tCitasDisponibles
                .Where(c => c.Estado == true && c.Fecha > ahoraLocal)
                .OrderBy(c => c.Fecha)
                .ToList();

            ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");

            var respuesta = citasM.CitasProgramadas();
            return View(respuesta);
        }



        [FiltroAdmin]
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

        [FiltroAdmin]
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

        [HttpPost]
        public JsonResult EjecutarSpInsertarCitasDisponibles()
        {
            try
            {
                CitasModel citasModel = new CitasModel();
                bool resultado = citasModel.EjecutarInsertarCitasDisponibles();

                return Json(new
                {
                    success = resultado,
                    message = resultado ? "Citas disponibles insertadas correctamente." : "No se insertaron las citas disponibles."
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocurrió un error: " + ex.Message });
            }
        }




    }
}