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

        // Propiedad añadida para el nivel mínimo de stock
        public int NivelMinimoStock { get; set; }
    }
}
