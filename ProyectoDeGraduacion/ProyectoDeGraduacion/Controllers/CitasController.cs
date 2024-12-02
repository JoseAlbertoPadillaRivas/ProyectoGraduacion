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


        //[HttpGet]
        //public ActionResult RegistrarCita()
        //{
        //    // Obtén el valor de `Session` para `idPaciente`
        //    int idPaciente = (int)Session["idUsuario"];

        //    // Crear una instancia del modelo Citas y asignar el valor de `idPaciente`
        //    var model = new Citas
        //    {
        //        idPaciente = idPaciente
        //    };

        //    // Obtener la lista de servicios y pasarla a la vista
        //    var sedes = _context.tSede.ToList();
        //    ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");


        //    // Obtener la lista de servicios y pasarla a la vista
        //    var citasDisponibles = _context.tCitasDisponibles
        //        .Where(c => c.Estado == true) // Filtrar citas disponibles (Estado == 1)
        //        .ToList();
        //    ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");


        //    return View(model); // Pasar el modelo con `idPaciente` a la vista
        //}


        [HttpPost]
        public ActionResult RegistrarCita(Citas cita)
        {
            var respuesta = citasM.RegistrarCita(cita);
            if (respuesta)
                
                return RedirectToAction("MisCitas", "Citas");
            else
            {
                ViewBag.msj = "Error al registrar la cita";
                return View();
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
        public ActionResult ReprogramarCita(Citas cita)
        {
            var respuesta = citasM.ReprogramarCita(cita);
            
            if (respuesta)
                return RedirectToAction("CitasProgramadas", "Citas");
            else
            {
                ViewBag.msj = "No se ha podido actualizar la informacion de la cita";
                return View();
            }            
        }

        [HttpPost]
        public ActionResult CancelarCita(int idCita)
        {
            var respuesta = citasM.CancelarCita(idCita);

            if (respuesta)
            {
                return Json(new { success = true, message = "Cita cancelada correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Hubo un error al cancelar la cita." });
            }
        }


    }
}