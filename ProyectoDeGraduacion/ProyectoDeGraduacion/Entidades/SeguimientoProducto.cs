using System;

namespace ProyectoDeGraduacion.Entidades
{
    public class SeguimientoProducto
    {
        public int idSeguimiento { get; set; }
        public int idPaciente { get; set; }
        public int idProducto { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public string Estado { get; set; }

        // Agregar propiedad de navegación para la relación con Pacientes
        public virtual Pacientes Paciente { get; set; }
    }
}