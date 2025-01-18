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
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class HistorialController : Controller
    {
        HistorialModel historialM = new HistorialModel();
        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();


        [HttpGet]
        public ActionResult MiHistorial()
        {
            var respuesta = historialM.ConsultarHistorialIDUsuario(int.Parse(Session["idUsuario"].ToString()));
            return View(respuesta);
        }

        [FiltroAdmin]
        [HttpGet]
        public ActionResult RegistrarHistoria()
        {
            var pacientes = _context.tPacientes.ToList();
            ViewBag.NombrePaciente = new SelectList(pacientes, "idPaciente", "Nombre");
            return View();
        }


        [FiltroAdmin]
        [HttpPost]
        public ActionResult RegistrarHistoria(HttpPostedFileBase Archivo, tHistorial hist)
        {
            try
            {
                if (Archivo == null || Archivo.ContentLength == 0)
                {
                    ViewBag.msj = "Por favor, selecciona un archivo.";
                    return View();
                }

                // Validar y crear la carpeta si no existe
                string directorio = Server.MapPath("~/ArchivosHistorial");
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                // Generar el nombre y la ruta completa del archivo
                string extension = Path.GetExtension(Archivo.FileName);
                string fechaCreacion = DateTime.Now.ToString("yyyyMMdd");
                string idUnico = new Random().Next(10000, 99999).ToString();
                string nombreArchivo = $"HistorialMedico_{fechaCreacion}_{idUnico}{extension}";
                string ruta = Path.Combine(directorio, nombreArchivo);

                // Guardar el archivo en la ruta
                Archivo.SaveAs(ruta);

                // Guardar la ruta del archivo en la base de datos
                hist.Archivo = "/ArchivosHistorial/" + nombreArchivo;
                var consecutivo = historialM.RegistrarHistoria(hist);

                if (consecutivo > 0)
                {
                    return RedirectToAction("HistorialesAdmin", "Historial");
                }
                else
                {
                    ViewBag.historial = "No se ha podido registrar la información del producto.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Registrar errores para depuración
                System.Diagnostics.Debug.WriteLine("Error en RegistrarHistoria: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("StackTrace: " + ex.StackTrace);

                ViewBag.Error = "Ocurrió un error al procesar la solicitud. Por favor, inténtalo más tarde.";
                return View();
            }
        }


        [FiltroAdmin]
        [HttpGet]
        public ActionResult ActualizarHistorial(int idHistorial)
        {
            var pacientes = _context.tPacientes.Select(p => new { p.idPaciente, p.Nombre }).ToList();
            ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
            var respuesta = historialM.ConsultarHistorialID(idHistorial);
            return View(respuesta);
        }
        [FiltroAdmin]
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

        [HttpGet]
        public ActionResult HistorialPaciente(int idPaciente)
        {
            var respuesta = historialM.ConsultarHistorialIDPaciente(idPaciente);
            return View(respuesta);
        }

    }
}