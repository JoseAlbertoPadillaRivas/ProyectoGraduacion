﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Entidades
{
    public class Pacientes
    {
        public int idPaciente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public bool Estado { get; set; }
        public byte idRol { get; set; }
    }
}