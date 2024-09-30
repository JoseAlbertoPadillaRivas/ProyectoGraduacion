using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Models
{
    public class SeguimientoModel
    {
        public bool RegistrarSeguimiento(Seguimiento seguimiento)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.RegistrarSeguimiento(seguimiento.idPaciente, seguimiento.Nombre, seguimiento.FechaEntrega);
            }

            return (rowsAffected > 0 ? true : false);
        }
        public List<tSeguimiento> ConsultarSeguimiento()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tSeguimiento
                        select x).ToList();
            }
        }

        public tSeguimiento ConsultarSeguimientoID(int idSeguimiento)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tSeguimiento
                        where x.idSeguimiento == idSeguimiento
                        select x).FirstOrDefault();
            }
        }

        public List<tSeguimiento> ConsultarMisProductos(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {

                return (from x in context.tSeguimiento
                        where x.idPaciente == idPaciente
                        select x).ToList();
            }
        }

        public bool EliminarSeguimiento(Seguimiento seguimiento)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.EliminarSeguimiento(seguimiento.idSeguimiento);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool ActualizarSeguimiento(Seguimiento seguimiento)
        {
            
                var rowsAffected = 0;

                using (var context = new ProyectoGraduacionEntities())
                {
                    rowsAffected = context.ActualizarSeguimiento(seguimiento.idPaciente, seguimiento.Nombre, seguimiento.FechaEntrega, seguimiento.idSeguimiento);
                }

                return (rowsAffected > 0 ? true : false);
            
        }
        public bool CambiarEstadoSeguimiento(Seguimiento seguimiento)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.CambiarEstadoSeguimiento(seguimiento.idSeguimiento);
            }

            return (rowsAffected > 0 ? true : false);
        }
    }
}