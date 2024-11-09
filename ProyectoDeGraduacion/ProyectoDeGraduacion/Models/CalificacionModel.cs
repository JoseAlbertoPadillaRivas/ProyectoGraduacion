using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Models
{
    public class CalificacionModel
    {
        public bool NuevaCalificacion(Calificacion calificacion)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.GenerarCalificacion(calificacion.Calificaciones, calificacion.idPaciente, calificacion.Opinion, calificacion.idServicio, calificacion.Fecha);
            }

            return (rowsAffected > 0 ? true : false);
        }

        //public bool NuevaCalificacion(Calificacion calificacion)
        //{
        //    using (var context = new ProyectoGraduacionEntities())
        //    {
        //        tCalificaciones nuevoCalificacion = new tCalificaciones
        //        {
        //            Calificaciones = calificacion.Calificaciones,
        //            idPaciente = calificacion.idPaciente,
        //            Opinion = calificacion.Opinion,
        //            idServicio = calificacion.idServicio,
        //            Fecha = calificacion.Fecha  
        //        };

        //        context.tCalificaciones.Add(nuevoCalificacion);
        //        context.SaveChanges();
        //    }

        //    return true;
        //}

        public List<Calificacion> verCalificaciones()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var query = from calificacion in context.tCalificaciones
                            join paciente in context.tPacientes on calificacion.idPaciente equals paciente.idPaciente
                            join servicio in context.tServicio on calificacion.idServicio equals servicio.idServicio
                            select new Calificacion
                            {
                                idCalificaciones = calificacion.idCalificaciones,
                                Calificaciones = calificacion.Calificaciones,
                                idPaciente = calificacion.idPaciente,
                                Opinion = calificacion.Opinion,
                                NombrePaciente = paciente.Nombre,// Nombre del paciente
                                NombreServicio = servicio.Nombre// Nombre del servicio
                            };

                return query.ToList();
            }
        }

        public bool EliminarCalificacion(int idCalificaciones)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var calificacion = context.tCalificaciones.FirstOrDefault(c => c.idCalificaciones == idCalificaciones);

                if (calificacion != null)
                {
                    context.tCalificaciones.Remove(calificacion);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public List<Calificacion> ObtenerCalificaciones()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var reporteTemporal = context.tCalificaciones
                    .GroupBy(c => new { c.Fecha.Year, c.Fecha.Month })
                    .Select(g => new
                    {
                        CalificacionesTotales = g.Count(),
                        PromedioCalificaciones = g.Average(c => c.Calificaciones)
                    })
                    .ToList();

                var reporte = reporteTemporal
                    .Select(r => new Calificacion
                    {
                        Fecha = DateTime.Today,  // Establece la fecha a la fecha de hoy
                        CalificacionesTotales = r.CalificacionesTotales,
                        PromedioCalificaciones = r.PromedioCalificaciones
                    })
                    .ToList();

                return reporte;
            }
        }



    }
}