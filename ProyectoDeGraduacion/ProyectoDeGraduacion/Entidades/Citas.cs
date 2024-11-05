using System;


namespace ProyectoDeGraduacion.Entidades
{
    public class Citas
    {
        public int idCita { get; set; }

        public int idPaciente { get; set; }

        public int idSede { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora{ get; set; }

        public String NombrePaciente{ get; set; }

        public String NombreSede { get; set; }
    }
}