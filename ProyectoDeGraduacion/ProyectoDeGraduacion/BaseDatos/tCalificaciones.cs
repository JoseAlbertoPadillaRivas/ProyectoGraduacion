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
    
    public partial class tCalificaciones
    {
        public int idCalificaciones { get; set; }
        public int Calificaciones { get; set; }
        public int idPaciente { get; set; }
        public string Opinion { get; set; }
        public Nullable<int> idServicio { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual tPacientes tPacientes { get; set; }
        public virtual tServicio tServicio { get; set; }
    }
}
