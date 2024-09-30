using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class Seguimiento
    {
        public int idSeguimiento { get; set; }

        public int idPaciente { get; set; }
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaEntrega { get; set; }

    }
}