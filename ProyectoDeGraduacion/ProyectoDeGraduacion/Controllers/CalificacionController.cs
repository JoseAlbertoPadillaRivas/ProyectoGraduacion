using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using Rotativa;

namespace ProyectoDeGraduacion.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class CalificacionController : Controller
    {

        private ProyectoGraduacionEntities db = new ProyectoGraduacionEntities();

        PacientesModel pacienteM = new PacientesModel();
        CalificacionModel calificacionM = new CalificacionModel();

        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();

        [HttpGet]
        public ActionResult verCalificaciones()
        {
            var respuesta = calificacionM.verCalificaciones();
            return View(respuesta);
        }
        
        [HttpGet]
        [FiltroSeguridad]
        public ActionResult NuevaCalificacion()
        {
            // Obtener el idPaciente de la sesión
            int idPaciente = (int)Session["idUsuario"];

            // Crear una instancia del modelo Calificacion y asignar el valor de idPaciente
            var model = new Calificacion
            {
                idPaciente = idPaciente
            };

            // Obtener la lista de servicios y pasarla a la vista
            var servicios = _context.tServicio.ToList();
            ViewBag.Servicio = new SelectList(servicios, "idServicio", "Nombre");

            return View(model); 
        }

        [FiltroSeguridad]
        [HttpPost]
        public ActionResult NuevaCalificacion(Calificacion calificacion)
        {
            var respuesta = calificacionM.NuevaCalificacion(calificacion);

            if (respuesta)
                return RedirectToAction("verCalificaciones", "Calificacion");
            else
            {
                ViewBag.msj = "No se ha podido actualizar su información de perfil";
                return View();
            }
        }
        [FiltroSeguridad]
        [FiltroAdmin]
        [HttpPost]
        public ActionResult EliminarCalificacion(int idCalificaciones)
        {
            var respuesta = calificacionM.EliminarCalificacion(idCalificaciones);

            if (respuesta)
            {
                return RedirectToAction("AdminCalificacion", "Calificacion");
            }
            else
            {
                return RedirectToAction("AdminCalificacion", "Calificacion");
            }
        }

        [FiltroSeguridad]
        [FiltroAdmin]
        [HttpGet]
        public ActionResult AdminCalificacion()
        {
            var respuesta = calificacionM.verCalificaciones();
            return View(respuesta);
        }

        [FiltroSeguridad]
        [FiltroAdmin]
        [HttpGet]
        public ActionResult AnalizarCalificaciones()
        {
            var calificacionModel = new CalificacionModel();
            var reporteCalificaciones = calificacionModel.ObtenerCalificaciones();

            return View(reporteCalificaciones);
        }


        [FiltroAdmin]
        public ActionResult CalificacionPDF()
        {
            var calificacion = (from calificaciones in db.tCalificaciones
                                    join paciente in db.tPacientes on calificaciones.idPaciente equals paciente.idPaciente
                                    join servicio in db.tServicio on calificaciones.idServicio equals servicio.idServicio
                                    select new Calificacion
                                    {
                                        idCalificaciones = calificaciones.idCalificaciones,
                                        Calificaciones = calificaciones.Calificaciones,
                                        idPaciente = calificaciones.idPaciente,
                                        Opinion = calificaciones.Opinion,
                                        NombrePaciente = paciente.Nombre, // Nombre del paciente
                                        NombreServicio = servicio.Nombre  // Nombre del servicio
                                    }).ToList();

            return View(calificacion);
        }

    }
}