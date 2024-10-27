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
                //return context.ConsultarProductos().ToList();
            }
        }
        public tHistorial ConsultarHistorialID(int Consecutivo)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.tHistorial.Where(x => x.idHistorial == Consecutivo).FirstOrDefault();
            }
        }

        public int RegistrarHistoria(tHistorial hist)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.RegistrarHistorial(
                    hist.idPaciente,
                    hist.FechaConsulta,
                    hist.Diagnostico,
                    hist.Tratamiento,
                    hist.Medicacion,
                    hist.Observaciones,
                    hist.Archivo);
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
    }
}