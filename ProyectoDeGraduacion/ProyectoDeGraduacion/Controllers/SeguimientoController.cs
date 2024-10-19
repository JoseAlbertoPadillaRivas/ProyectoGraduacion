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
            var productos = _context.tInventario.ToList();
            ViewBag.Productos = new SelectList(productos, "idProducto", "NombreProducto");
            return View();
        }

        // Agregar producto al seguimiento
        [HttpPost]
        public ActionResult AgregarProducto(SeguimientoProducto seguimiento)
        {
            var respuesta = seguimientoM.RegistrarSeguimiento(seguimiento);

            if (respuesta)
                return RedirectToAction("MostrarProductos", "Seguimiento");
            else
            {
                ViewBag.msj = "Error al registrar información";
                var pacientes = _context.tPacientes.ToList();
                ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
                var productos = _context.tInventario.ToList();
                ViewBag.Productos = new SelectList(productos, "idProducto", "NombreProducto");
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
        public bool CambiarEstadoSeguimiento(SeguimientoProducto seguimiento)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                // Obtener el seguimiento a modificar
                var seguimientoExistente = context.tSeguimientoProducto.FirstOrDefault(s => s.idSeguimiento == seguimiento.idSeguimiento);

                if (seguimientoExistente != null)
                {
                    // Cambiar el estado
                    seguimientoExistente.Estado = seguimiento.Estado;

                    // Guardar los cambios
                    rowsAffected = context.SaveChanges();
                }
            }

            return (rowsAffected > 0 ? true : false);
        }

        // Vista para actualizar un producto en seguimiento
        //[FiltroAdmin]
        //[HttpGet]
        //public ActionResult ActualizarSeguimiento(int idSeguimiento)
        //{
        //    var pacientes = _context.tPacientes.ToList();
        //    ViewBag.Pacientes = new SelectList(pacientes, "idPaciente", "Nombre");
        //    var productos = _context.tInventario.ToList();
        //    ViewBag.Productos = new SelectList(productos, "idProducto", "NombreProducto");

        //    var respuesta = seguimientoM.ConsultarSeguimientoID(idSeguimiento);
        //    return View(respuesta);
        //}

        // Actualizar un producto en seguimiento
        [HttpPost]
        public ActionResult ActualizarSeguimiento(SeguimientoProducto seguimiento)
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
        public ActionResult EliminarSeguimiento(SeguimientoProducto seguimiento)
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
