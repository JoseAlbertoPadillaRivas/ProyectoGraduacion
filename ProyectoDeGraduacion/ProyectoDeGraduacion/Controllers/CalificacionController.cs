using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;

namespace ProyectoDeGraduacion.Controllers
{
    public class CalificacionController : Controller
    {

        PacientesModel pacienteM = new PacientesModel();
        CalificacionModel calificacionM = new CalificacionModel();

        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();

        [HttpGet]
        public ActionResult verCalificaciones()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult NuevaCalificacion()
        {
            // Obtén el valor de `Session` para `idPaciente`
            int idPaciente = (int)Session["idUsuario"];

            // Crear una instancia del modelo Calificacion y asignar el valor de `idPaciente`
            var model = new Calificacion
            {
                idPaciente = idPaciente
            };

            // Obtener la lista de servicios y pasarla a la vista
            var servicios = _context.tServicio.ToList();
            ViewBag.Servicio = new SelectList(servicios, "idServicio", "Nombre");

            return View(model); // Pasar el modelo con `idPaciente` a la vista
        }


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

        [HttpGet]
        public ActionResult AdminCalificacion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AnalizarCalificaciones()
        {
            return View();
        }

    }
}