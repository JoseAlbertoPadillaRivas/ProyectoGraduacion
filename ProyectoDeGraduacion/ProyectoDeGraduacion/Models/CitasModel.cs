using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Models
{
    public class CitasModel
    {
        public bool RegistrarCita(Citas cita)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.RegistrarCita(cita.idPaciente, cita.idSede, cita.Fecha, cita.Hora);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public List<tCitas> MisCitas(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tCitas
                        where x.idPaciente == idPaciente
                        select x).ToList();
            }
        }

        public List<Citas> CitasProgramadas()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var query = from cita in context.tCitas
                            join paciente in context.tPacientes on cita.idPaciente equals paciente.idPaciente
                            join sede in context.tSede on cita.idSede equals sede.idSede
                            select new Citas
                            {
                                idCita = cita.idCita,
                                idPaciente = cita.idPaciente,
                                idSede = cita.idSede,
                                Fecha = cita.Fecha,
                                Hora = cita.Hora,
                                NombrePaciente = paciente.Nombre, // Nombre del paciente
                                NombreSede = sede.Nombre // Nombre de la sede
                            };

                return query.ToList();
            }
        }

    }


}



