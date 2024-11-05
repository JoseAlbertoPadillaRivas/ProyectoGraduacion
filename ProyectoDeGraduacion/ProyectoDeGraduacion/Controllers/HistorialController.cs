using System.IO;
using System.Web;
using System;
using System.Web.Mvc;
using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Models;
using ProyectoDeGraduacion.Entidades;
using System.Linq;

namespace ProyectoDeGraduacion.Controllers
{
    public class HistorialController : Controller
    {
        HistorialModel historialM = new HistorialModel();
        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();


        [HttpGet]
        public ActionResult HistorialPaciente()
        {
            var respuesta = historialM.ConsultarHistorialIDPaciente(int.Parse(Session["idUsuario"].ToString()));
            return View(respuesta);
        }

        [HttpGet]
        public ActionResult HistorialesAdmin()
        {
            var respuesta = historialM.ConsultarHistoriales();
            return View(respuesta);
        }

        [HttpGet]
        public ActionResult RegistrarHistoria()
        {
            var pacientes = _context.tPacientes.ToList();
            ViewBag.NombrePaciente = new SelectList(pacientes, "idPaciente", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarHistoria(HttpPostedFileBase Archivo, tHistorial hist, tPacientes paciente)
        {
            if (Archivo == null || Archivo.ContentLength == 0)
            {
                ViewBag.msj = "Por favor, selecciona un archivo.";
                return View();
            }

            string extension = Path.GetExtension(Path.GetFileName(Archivo.FileName));



            //PRUEBA DE RENOMBRAR EL ARCHIVO CON UN NOMBRE LEGIBLE Y UNICO, ESTA NO ES EL NOMBRE FINAL, LO IDEAL SERIA PONER EL NOMBRE DEL PACIENTE
            string nombrePaciente = hist.idHistorial.ToString();
            nombrePaciente = string.Join("_", nombrePaciente.Split(Path.GetInvalidFileNameChars()));

            string nombreArchivo = $"{nombrePaciente}_{new Random().Next(10000, 99999)}{extension}";
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ArchivosHistorial", nombreArchivo);

            Archivo.SaveAs(ruta);

            hist.Archivo = "/ArchivosHistorial/" + nombreArchivo;

            var consecutivo = historialM.RegistrarHistoria(hist);

            if (consecutivo > 0)
            {
                return RedirectToAction("HistorialesAdmin", "Historial");
            }
            else
            {
                ViewBag.msj = "No se ha podido registrar la información del producto";
                return View();
            }
        }
        [HttpGet]
        public ActionResult ActualizarHistorial(int idHistorial)
        {
            var pacientes = _context.tPacientes.Select(p => new { p.idPaciente, p.Nombre }).ToList();
            ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
            var respuesta = historialM.ConsultarHistorialID(idHistorial);
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult ActualizarHistorial(tHistorial historial)
        {
            var respuesta = historialM.ActualizarHistorial(historial);

            if (respuesta)
                return RedirectToAction("HistorialesAdmin", "Historial");
            else
            {
                ViewBag.msj = "Error al actualizar";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EliminarHistoria(int idHistorial)
        {
            var respuesta = historialM.EliminarHistorial(idHistorial);

            if (respuesta)
            {
                return RedirectToAction("HistorialesAdmin", "Historial");
            }
            else
            {
                return RedirectToAction("HistorialesAdmin", "Historial");
            }
        }






    }
}