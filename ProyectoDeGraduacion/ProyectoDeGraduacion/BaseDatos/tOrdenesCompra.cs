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
    
    public partial class tOrdenesCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tOrdenesCompra()
        {
            this.tOrdenesProductos = new HashSet<tOrdenesProductos>();
        }
    
        public int idOrdenCompra { get; set; }
        public int idProveedor { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadTotalSolicitada { get; set; }
        public string EstadoOrden { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public int idProducto { get; set; }
    
        public virtual tInventario tInventario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrdenesProductos> tOrdenesProductos { get; set; }
        public virtual tProveedores tProveedores { get; set; }
    }
}
