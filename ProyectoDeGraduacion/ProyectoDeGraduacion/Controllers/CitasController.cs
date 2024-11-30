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


        [HttpGet]
        public ActionResult RegistrarCita()
        {
            // Obtén el valor de `Session` para `idPaciente`
            int idPaciente = (int)Session["idUsuario"];

            // Crear una instancia del modelo Citas y asignar el valor de `idPaciente`
            var model = new Citas
            {
                idPaciente = idPaciente
            };

            // Obtener la lista de servicios y pasarla a la vista
            var sedes = _context.tSede.ToList();
            ViewBag.Sede = new SelectList(sedes, "idSede", "Nombre");


            // Obtener la lista de servicios y pasarla a la vista
            var citasDisponibles = _context.tCitasDisponibles
                .Where(c => c.Estado == true) // Filtrar citas disponibles (Estado == 1)
    .ToList();
            ViewBag.CitasDisponibles = new SelectList(citasDisponibles, "idCitaDisponible", "Fecha");


            return View(model); // Pasar el modelo con `idPaciente` a la vista
        }


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

            // Llamar al método y pasar el idPaciente
            var respuesta = citasM.MisCitas(idPaciente);

            return View(respuesta);
        }


        [HttpGet]
        public ActionResult CitasProgramadas()
        {
            var respuesta = citasM.CitasProgramadas();
            return View(respuesta);
        }

        [HttpGet]
        public ActionResult ReprogramarCita()
        {
            return View();
        }
    }
}