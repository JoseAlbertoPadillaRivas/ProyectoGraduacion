using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ProyectoDeGraduacion.Entidades
{
    public class Calificacion
    {
        public int idCalificaciones { get; set; }
        public int Calificaciones { get; set; }
        public int idPaciente { get; set; }
        public string Servicio { get; set; }
        public string Opinion { get; set; }
        public int idServicio { get; set; }
    }
}
