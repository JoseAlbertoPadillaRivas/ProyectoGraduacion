using System;

namespace ProyectoDeGraduacion.Entidades
{
    public class ProductoPendiente
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadSolicitada { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
