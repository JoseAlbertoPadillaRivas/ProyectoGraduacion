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
    
    public partial class tCalificaciones
    {
        public int idCalificaciones { get; set; }
        public int Calificaciones { get; set; }
        public int idPaciente { get; set; }
        public string Opinion { get; set; }
        public Nullable<int> idServicio { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual tPacientes tPacientes { get; set; }
        public virtual tPacientes tPacientes1 { get; set; }
        public virtual tPacientes tPacientes2 { get; set; }
        public virtual tServicio tServicio { get; set; }
        public virtual tServicio tServicio1 { get; set; }
        public virtual tServicio tServicio2 { get; set; }
    }
}
