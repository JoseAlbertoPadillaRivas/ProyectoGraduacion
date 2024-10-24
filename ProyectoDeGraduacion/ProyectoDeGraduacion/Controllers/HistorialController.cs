using System.IO;
using System.Web;
using System;
using System.Web.Mvc;
using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Models;

namespace ProyectoDeGraduacion.Controllers
{
    public class HistorialController : Controller
    {
        HistorialModel historialM = new HistorialModel();
        // GET: Historial
        public ActionResult IndexHistorial()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IndexHistorialAdmin()
        {
            var respuesta = historialM.ConsultarHistoriales();
            return View(respuesta);
        }

        [HttpGet]
        public ActionResult RegistrarHistoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarHistoria(HttpPostedFileBase Archivo, tHistorial hist)
        {
            var consecutivo = historialM.RegistrarHistoria(hist);

            if (consecutivo > 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(Archivo.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "ArchivosHistorial\\" + consecutivo + extension;
                Archivo.SaveAs(ruta);

                hist.idHistorial = consecutivo;
                hist.Archivo = "/ArchivosHistorial/" + consecutivo + extension;
                historialM.ActualizarArchivo(hist);

                return RedirectToAction("IndexHistorial", "Historial");
            }
            else
            {
                ViewBag.msj = "No se ha podido registrar la información del producto";
                return View();
            }
        }

        // GET: Historial/ModificarHistoria
        public ActionResult ModificarHistoria()
        {
            return View();
        }

        // GET: Historial/AñadirDocumentos
        public ActionResult AñadirDocumentos()
        {
            return View();
        }

        // GET: Historial/VerHistoria
        public ActionResult VerHistoria()
        {
            return View();
        }

        // GET: Historial/DescargarDocumentos
        public ActionResult DescargarDocumentos()
        {
            return View();
        }
    }
}
