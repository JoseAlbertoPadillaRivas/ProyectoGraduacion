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
    
    public partial class tInventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tInventario()
        {
            this.tMovimientosInventario = new HashSet<tMovimientosInventario>();
            this.tOrdenesProductos = new HashSet<tOrdenesProductos>();
            this.tProductosTratamiento = new HashSet<tProductosTratamiento>();
            this.tSeguimientoProducto = new HashSet<tSeguimientoProducto>();
        }
    
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public Nullable<System.DateTime> CaducidadProducto { get; set; }
        public int idProveedor { get; set; }
        public int NivelMinimoStock { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMovimientosInventario> tMovimientosInventario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrdenesProductos> tOrdenesProductos { get; set; }
        public virtual tProveedores tProveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tProductosTratamiento> tProductosTratamiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tSeguimientoProducto> tSeguimientoProducto { get; set; }
    }
}
