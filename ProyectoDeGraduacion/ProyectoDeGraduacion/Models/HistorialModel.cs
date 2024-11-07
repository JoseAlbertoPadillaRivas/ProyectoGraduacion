using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace ProyectoDeGraduacion.Models
{
    public class HistorialModel
    {
        public List<tHistorial> ConsultarHistoriales()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.tHistorial.ToList();
            }
        }
        public List<tHistorial> ConsultarHistorialIDPaciente(int idUsuario)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.tHistorial.Where(x => x.idPaciente == idUsuario).ToList();
            }
        }

        public tHistorial ConsultarHistorialID(int idHistorial)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.tHistorial.FirstOrDefault(x => x.idHistorial == idHistorial); // Devuelve un solo objeto
            }
        }

        public int RegistrarHistoria(tHistorial hist)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                tHistorial nuevoHistorial = new tHistorial
                {
                    idPaciente = hist.idPaciente,
                    FechaConsulta = hist.FechaConsulta,
                    Diagnostico = hist.Diagnostico,
                    Tratamiento = hist.Tratamiento,
                    Medicacion = hist.Medicacion,
                    Observaciones = hist.Observaciones,
                    Archivo = hist.Archivo


                };

                context.tHistorial.Add(nuevoHistorial);
                context.SaveChanges();

                return 1;
            }
        }


        public bool ActualizarArchivo(tHistorial hist)
        {
            try
            {
                var rowsAffected = 0;
                using (var context = new ProyectoGraduacionEntities())
                {
                    rowsAffected = context.ActualizarArchivo(hist.idHistorial, hist.Archivo);
                    System.Diagnostics.Debug.WriteLine("Filas afectadas: " + rowsAffected);
                }
                return (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al actualizar archivo: " + ex.Message);
                return false;
            }
        }


        public bool ActualizarHistorial(tHistorial hist)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var historial = context.tHistorial.FirstOrDefault(p => p.idHistorial == hist.idHistorial);

                if (historial != null)
                {
                    historial.idPaciente = hist.idPaciente;
                    historial.FechaConsulta = hist.FechaConsulta;
                    historial.Diagnostico = hist.Diagnostico;
                    historial.Tratamiento = hist.Tratamiento;
                    historial.Medicacion = hist.Medicacion;
                    historial.Observaciones = hist.Observaciones;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        public bool EliminarHistorial(int idHistorial)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var historia = context.tHistorial.FirstOrDefault(p => p.idHistorial == idHistorial);

                if (historia != null)
                {
                    context.tHistorial.Remove(historia);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}