using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class Inventario
    {
        public int idProducto { get; set; }
        public string NombreProducto { get; set;}

        public int Cantidad { get; set; }
        public DateTime CaducidadProducto{ get; set; }

        public int idProveedor { get; set; }

    }
}