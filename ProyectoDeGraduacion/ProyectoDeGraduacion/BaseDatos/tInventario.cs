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
    
    public partial class tInventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tInventario()
        {
            this.tOrdenesCompra = new HashSet<tOrdenesCompra>();
        }
    
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public Nullable<System.DateTime> CaducidadProducto { get; set; }
        public int idProveedor { get; set; }
        public int NivelMinimoStock { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrdenesCompra> tOrdenesCompra { get; set; }
        public virtual tProveedores tProveedores { get; set; }
    }
}
