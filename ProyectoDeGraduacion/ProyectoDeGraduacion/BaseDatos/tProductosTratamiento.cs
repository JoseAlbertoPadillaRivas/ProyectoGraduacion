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
    
    public partial class tProductosTratamiento
    {
        public int idProductoTratamiento { get; set; }
        public int idProducto { get; set; }
        public int idCita { get; set; }
        public int CantidadUtilizada { get; set; }
    
        public virtual tCitas tCitas { get; set; }
        public virtual tInventario tInventario { get; set; }
    }
}
