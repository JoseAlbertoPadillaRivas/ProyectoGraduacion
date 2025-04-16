using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
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

        public bool IsUserSessionAvailable(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var user = context.tPacientes.FirstOrDefault(x => x.idPaciente == idPaciente);

                if (user == null)
                    return true;

                // Si no hay token, el usuario puede iniciar sesión
                if (string.IsNullOrEmpty(user.SessionToken))
                    return true;

                // Si el token ha expirado, lo limpiamos
                if (user.SessionTokenExpira != null && user.SessionTokenExpira < DateTime.Now)
                {
                    user.SessionToken = null;
                    user.SessionTokenExpira = null;
                    context.SaveChanges();
                    return true;
                }

                // Token aún válido
                return false;
            }
        }


        public void UpdateUserSessionToken(int idPaciente, string token)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var user = context.tPacientes.FirstOrDefault(x => x.idPaciente == idPaciente);
                if (user != null)
                {
                    user.SessionToken = token;
                    if (token != null)
                        user.SessionTokenExpira = DateTime.Now.AddMinutes(30);
                    else
                        user.SessionTokenExpira = null;
                    context.SaveChanges();
                }
            }
        }

    }
}

