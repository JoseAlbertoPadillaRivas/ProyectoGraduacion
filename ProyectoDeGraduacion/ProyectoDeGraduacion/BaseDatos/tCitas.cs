//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoDeGraduacion.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tCitas
    {
        public int idCita { get; set; }
        public int idPaciente { get; set; }
        public int idSede { get; set; }
        public int idCitaDisponible { get; set; }
    
        public virtual tPacientes tPacientes { get; set; }
        public virtual tSede tSede { get; set; }
        public virtual tCitasDisponibles tCitasDisponibles { get; set; }
    }
}
