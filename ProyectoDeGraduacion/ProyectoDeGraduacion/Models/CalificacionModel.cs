using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Models
{
    public class CalificacionModel
    {
        public bool NuevaCalificacion(Calificacion calificacion)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.GenerarCalificacion(calificacion.Calificaciones, calificacion.idPaciente, calificacion.Servicio, calificacion.Opinion);
            }

            return (rowsAffected > 0 ? true : false);
        }
    }
}