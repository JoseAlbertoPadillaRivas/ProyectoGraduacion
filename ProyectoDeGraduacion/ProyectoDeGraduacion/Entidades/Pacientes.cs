using System.Collections.Generic;

namespace ProyectoDeGraduacion.Entidades
{
    public class Pacientes
    {
        public int idPaciente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public bool Estado { get; set; }
        public byte idRol { get; set; }

        // Agregar propiedad de navegación para los productos en seguimiento
        public virtual ICollection<Seguimiento> SeguimientoProductos { get; set; }
    }
}