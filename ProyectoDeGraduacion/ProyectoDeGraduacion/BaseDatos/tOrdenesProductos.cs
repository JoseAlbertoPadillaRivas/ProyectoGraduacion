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
    
    public partial class tOrdenesProductos
    {
        public int idOrdenProducto { get; set; }
        public int idOrdenCompra { get; set; }
        public int idProducto { get; set; }
        public int CantidadSolicitada { get; set; }
    
        public virtual tOrdenesCompra tOrdenesCompra { get; set; }
        public virtual tInventario tInventario { get; set; }
    }
}
