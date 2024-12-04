using System;

namespace ProyectoDeGraduacion.Entidades
{
    public class Seguimiento
    {
        public int idSeguimiento { get; set; }
        public int idPaciente { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool Estado { get; set; }
        public string NombreProducto { get; set; }
        public string Comentario { get; set; }

        // Agregar propiedad de navegación para la relación con Pacientes
        public virtual Pacientes Paciente { get; set; }

        public string NombrePaciente { get; set; }
    }
}