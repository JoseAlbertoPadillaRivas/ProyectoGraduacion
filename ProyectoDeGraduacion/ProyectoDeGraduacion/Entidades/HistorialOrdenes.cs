using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class HistorialOrdenes
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Proveedor { get; set; }
    }
}