using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoDeGraduacion.Models
{
    public class SeguimientoModel
    {

        public bool RegistrarSeguimiento(Seguimiento seguimiento)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                tSeguimiento nuevoProducto = new tSeguimiento
                {
                    idPaciente = seguimiento.idPaciente,
                    NombreProducto = seguimiento.NombreProducto,
                    Estado = true,
                    FechaEntrega = seguimiento.FechaEntrega

                };

                context.tSeguimiento.Add(nuevoProducto);
                context.SaveChanges(); 
            }

            return true;
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

        public bool EliminarSeguimiento(int idSeguimiento)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var seguimiento = context.tSeguimiento.FirstOrDefault(p => p.idSeguimiento == idSeguimiento);

                if (seguimiento != null)
                {
                    context.tSeguimiento.Remove(seguimiento);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool ActualizarSeguimiento(Seguimiento seguimiento)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var actualizarSeguimiento = context.tSeguimiento.FirstOrDefault(p => p.idSeguimiento == seguimiento.idSeguimiento);

                if (seguimiento != null)
                {
                    actualizarSeguimiento.NombreProducto = seguimiento.NombreProducto;
                    actualizarSeguimiento.idPaciente= seguimiento.idPaciente;
                    actualizarSeguimiento.FechaEntrega = seguimiento.FechaEntrega;

                    context.SaveChanges(); 
                    return true;
                }
                return false;
            }
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
