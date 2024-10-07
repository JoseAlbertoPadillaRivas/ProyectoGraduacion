using ProyectoDeGraduacion.Entidades;
using ProyectoDeGraduacion.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Models
{
    public class PacientesModel
    {
        public bool RegistrarPaciente(Pacientes paciente)
        {
                var rowsAffected = 0;

                using (var context = new ProyectoGraduacionEntities())
                {
                    rowsAffected = context.RegistrarPaciente(paciente.Cedula, paciente.Nombre, paciente.Correo, paciente.Contrasenna);
                }

                return (rowsAffected > 0 ? true : false);
        }

        public IniciarSesion_Result IniciarSesion(Pacientes paciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return context.IniciarSesion(paciente.Correo, paciente.Contrasenna).FirstOrDefault();
            }
        }

        public List<tPacientes> ConsultarPacientes()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                int idUsuario = int.Parse(HttpContext.Current.Session["idUsuario"].ToString());

                return (from x in context.tPacientes
                        where x.idPaciente != idUsuario
                        select x).ToList();
            }
        }

        public tPacientes ConsultarUsuarioidPaciente(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tPacientes
                        where x.idPaciente == idPaciente
                        select x).FirstOrDefault();
            }
        }

        public bool CambiarEstadoPaciente(Pacientes paciente)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.CambiarEstadoPaciente(paciente.idPaciente);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool ActualizarPaciente(Pacientes paciente)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.ActualizarPaciente(paciente.Cedula, paciente.Nombre, paciente.Correo, paciente.idRol, paciente.idPaciente);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool EliminarUsuario(Pacientes paciente)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.EliminarUsuario(paciente.idPaciente);    
            }

            return (rowsAffected > 0 ? true : false);
        }
    }
}

