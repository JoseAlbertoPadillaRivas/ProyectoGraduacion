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
    
    public partial class tSeguimiento
    {
        public int idSeguimiento { get; set; }
        public int idPaciente { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaEntrega { get; set; }
        public Nullable<bool> Estado { get; set; }
    
        public virtual tPacientes tPacientes { get; set; }
    }
}
