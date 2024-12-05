using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class Proveedor
    {
        public int idProveedor { get; set; }

        public string Empresa { get; set; }

        public string NumeroTelefono { get; set; }

        public string  Correo{ get; set; }

        public bool Estado { get; set; }
    }
}

