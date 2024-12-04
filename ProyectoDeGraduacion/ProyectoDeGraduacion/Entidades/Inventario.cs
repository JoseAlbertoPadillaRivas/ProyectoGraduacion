using System;

namespace ProyectoDeGraduacion.Entidades
{
    public class Inventario
    {
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime? CaducidadProducto { get; set; }
        public int idProveedor { get; set; }
        public int NivelMinimoStock { get; set; }

        // Nueva propiedad para mostrar el nombre del proveedor
        public string NombreProveedor { get; set; }
    }
}
