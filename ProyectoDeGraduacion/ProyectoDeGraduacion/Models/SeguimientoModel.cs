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
                    FechaEntrega = seguimiento.FechaEntrega,
                    Comentario = seguimiento.Comentario 
                };

                context.tSeguimiento.Add(nuevoProducto);
                context.SaveChanges();
            }

            return true;
        }

        public List<Seguimiento> ConsultarSeguimiento()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var seguimiento = (from seg in context.tSeguimiento
                                   join pac in context.tPacientes on seg.idPaciente equals pac.idPaciente
                                   select new Seguimiento
                                   {
                                       idSeguimiento = seg.idSeguimiento,
                                       idPaciente = seg.idPaciente,
                                       NombrePaciente = pac.Nombre,
                                       NombreProducto = seg.NombreProducto,
                                       FechaEntrega = seg.FechaEntrega,
                                       Estado = seg.Estado,
                                       Comentario = seg.Comentario
                                   }).ToList();

                return seguimiento;
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

        public List<Seguimiento> ConsultarMisProductos(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var datos = (from x in context.tSeguimiento
                             where x.idPaciente == idPaciente
                             select new Seguimiento
                             {
                                 idSeguimiento = x.idSeguimiento,
                                 idPaciente = x.idPaciente,
                                 NombrePaciente = x.tPacientes.Nombre,
                                 NombreProducto = x.NombreProducto,
                                 FechaEntrega = x.FechaEntrega,
                                 Estado = x.Estado,
                                 Comentario = x.Comentario
                             }).ToList();

                return datos;
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

                if (actualizarSeguimiento != null)
                {
                    actualizarSeguimiento.NombreProducto = seguimiento.NombreProducto;
                    actualizarSeguimiento.idPaciente = seguimiento.idPaciente;
                    actualizarSeguimiento.FechaEntrega = seguimiento.FechaEntrega;
                    actualizarSeguimiento.Comentario = seguimiento.Comentario; 

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
