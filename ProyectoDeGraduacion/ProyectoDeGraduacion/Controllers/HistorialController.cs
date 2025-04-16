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
        public ActionResult RegistrarHistoria(tHistorial hist, HttpPostedFileBase Archivo)
        {
            try
            {
                if (Archivo != null && Archivo.ContentLength > 0)
                {
                    // 1) Obtener extensión y construir nombre deseado
                    string extension = System.IO.Path.GetExtension(Archivo.FileName);
                    string nombreArchivo = "HistorialMedico_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + extension;

                    // 2) Crear la ruta física (Server.MapPath) a la carpeta "ArchivosHistorial"
                    string carpeta = Server.MapPath("~/ArchivosHistorial/");
                    string rutaCompleta = System.IO.Path.Combine(carpeta, nombreArchivo);

                    // 3) Guardar físicamente el archivo en esa ruta
                    Archivo.SaveAs(rutaCompleta);

                    // 4) Guardar en la propiedad 'Archivo' de tu entidad la RUTA RELATIVA
                    //    (es decir, la que comienza con "~").
                    hist.Archivo = "~/ArchivosHistorial/" + nombreArchivo;
                }

                // 5) Registrar en BD (este método llama a tu HistorialModel)
                var resultado = historialM.RegistrarHistoria(hist);

                // 6) Redireccionar donde quieras
                if (resultado == 1)
                {
                    // Éxito
                    return RedirectToAction("ConsultarPacientes", "Pacientes");
                }
                else
                {
                    // Hubo algún fallo
                    ViewBag.msj = "No se pudo registrar el historial.";
                    return View(hist);
                }
            }
            catch (Exception ex)
            {
                ViewBag.msj = "Ocurrió un error: " + ex.Message;
                return View(hist);
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
                return RedirectToAction("ConsultarPacientes", "Pacientes");
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

        [HttpGet]
        public ActionResult DescargarArchivo(int idHistorial)
        {
            var hist = historialM.ConsultarHistorialID(idHistorial);

            // hist.Archivo => "~/ArchivosHistorial/HistorialMedico_20250325_2241.pdf"
            string rutaFisica = Server.MapPath(hist.Archivo);

            if (!System.IO.File.Exists(rutaFisica))
            {
                // Manejar el caso de que no exista físicamente
                return HttpNotFound("El archivo no existe o fue movido.");
            }

            string nombreDescarga = System.IO.Path.GetFileName(rutaFisica);

            return File(rutaFisica, "application/pdf", nombreDescarga);
        }




    }
}