//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
        public string Estado { get; set; }
        public System.DateTime FechaEntrega { get; set; }
    
        public virtual tPacientes tPacientes { get; set; }
    }
}
