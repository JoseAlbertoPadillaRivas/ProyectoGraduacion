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


        [FiltroAdmin]
        [HttpGet]
        public ActionResult MostrarProductos()
        {
            var respuesta = seguimientoM.ConsultarSeguimiento();
            return View(respuesta);
        }
        [FiltroAdmin]
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            var pacientes = _context.tPacientes.ToList();
            ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.RegistrarSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al registrar informacion";
                var pacientes = _context.tPacientes.ToList();
                ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
                return View();
            }
        }
        [HttpGet]
        public ActionResult MisProductos()
        {
            var respuesta = seguimientoM.ConsultarMisProductos(int.Parse(Session["idUsuario"].ToString()));
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult CambiarEstadoSeguimiento(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.CambiarEstadoSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al desactivar";
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


        [HttpPost]
        public ActionResult EliminarSeguimiento(Seguimiento seguimiento)
        {
            var respuesta = seguimientoM.EliminarSeguimiento(seguimiento);

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