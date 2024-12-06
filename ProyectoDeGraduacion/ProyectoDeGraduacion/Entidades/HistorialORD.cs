using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class HistorialORD
    {
        public string NombreProducto { get; set; }
        public int CantidadTotalSolicitada { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string NombreProveedor { get; set; }

        public int idOrdenCompra { get; set; }
    }
}