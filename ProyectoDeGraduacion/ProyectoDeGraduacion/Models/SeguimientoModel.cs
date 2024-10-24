using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoDeGraduacion.Models
{
    public class SeguimientoModel
    {
        public bool RegistrarSeguimiento(SeguimientoProducto seguimiento)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                //// Ajusta el método RegistrarSeguimientoProducto con los campos correspondientes
                //rowsAffected = context.RegistrarSeguimientoProducto(seguimiento.idPaciente, seguimiento.idProducto, seguimiento.FechaEntregaEstimada, seguimiento.Estado);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public List<tSeguimientoProducto> ConsultarSeguimiento()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tSeguimientoProducto
                        select x).ToList();
            }
        }

        //public tSeguimientoProducto ConsultarSeguimientoID(int idSeguimiento)
        //{
        //    using (var context = new ProyectoGraduacionEntities())
        //    {
        //        //return (from x in context.tSeguimientoProducto
        //        //        where x.idSeguimiento == idSeguimiento
        //        //        select x).FirstOrDefault();
        //    }
        //}

        //public List<tSeguimientoProducto> ConsultarMisProductos(int idPaciente)
        //{
        //    using (var context = new ProyectoGraduacionEntities())
        //    {
        //        //return (from x in context.tSeguimientoProducto
        //        //        where x.idPaciente == idPaciente
        //        //        select x).ToList();
        //    }
        //}

        public bool EliminarSeguimiento(SeguimientoProducto seguimiento)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                //rowsAffected = context.EliminarSeguimientoProducto(seguimiento.idSeguimiento);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool ActualizarSeguimiento(SeguimientoProducto seguimiento)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                //rowsAffected = context.ActualizarSeguimientoProducto(seguimiento.idPaciente, seguimiento.idProducto, seguimiento.FechaEntregaEstimada, seguimiento.Estado, seguimiento.idSeguimiento);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool CambiarEstadoSeguimiento(SeguimientoProducto seguimiento)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                //rowsAffected = context.CambiarEstadoSeguimientoProducto(seguimiento.idSeguimiento);
            }

            return (rowsAffected > 0 ? true : false);
        }
    }
}
