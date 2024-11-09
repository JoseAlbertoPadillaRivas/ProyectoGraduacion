using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class SeguimientoController : Controller
    {
        SeguimientoModel seguimientoM = new SeguimientoModel();
        private ProyectoGraduacionEntities _context = new ProyectoGraduacionEntities();

        // Mostrar productos con seguimiento
        [FiltroAdmin]
        [HttpGet]
        public ActionResult MostrarProductos()
        {
            var respuesta = seguimientoM.ConsultarSeguimiento();
            return View(respuesta);
        }

        // Vista para agregar producto al seguimiento
        [FiltroAdmin]
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            var pacientes = _context.tPacientes.ToList();
            ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
            return View();
        }

        // Agregar producto al seguimiento
        [HttpPost]
        public ActionResult AgregarProducto(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.RegistrarSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al registrar información";
                var pacientes = _context.tPacientes.ToList();
                ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
                return View();
            }
        }

        // Mostrar productos del usuario logueado
        [HttpGet]
        public ActionResult MisProductos()
        {
            throw new NotImplementedException("Este método depende de un SP que aún no está disponible.");
        }

        // Cambiar estado del seguimiento de un producto
        [HttpPost]
        public ActionResult CambiarEstadoSeguimiento(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.CambiarEstadoSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "No se ha podido inactivar la información del usuario";
                return View();
            }
        }


        [FiltroAdmin]
       [HttpGet]
        public ActionResult ActualizarSeguimiento(int idSeguimiento)
        {
            var pacientes = _context.tPacientes.ToList();
            ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");

            var respuesta = seguimientoM.ConsultarSeguimientoID(idSeguimiento);
            return View(respuesta);
        }


       [HttpPost]
        public ActionResult ActualizarSeguimiento(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.ActualizarSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al actualizar";
                return View();
            }
        }

        // Eliminar un seguimiento de producto
        [HttpPost]
        public ActionResult EliminarSeguimiento(int idSeguimiento)
        {
            var respuesta = seguimientoM.EliminarSeguimiento(idSeguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al eliminar";
                return View();
            }
        }
    }
}
