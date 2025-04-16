using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Models
{
    public class CitasModel
    {
        public bool RegistrarCita(Citas cita)
        {
            try
            {
                using (var context = new ProyectoGraduacionEntities())
                {
                    // Registrar la cita utilizando el procedimiento almacenado
                    var rowsAffected = context.RegistrarCita(cita.idPaciente, cita.idSede, cita.idCitaDisponible);

                    if (rowsAffected > 0)
                    {
                        // Recuperar información de la cita registrada
                        var citaInfo = (from c in context.tCitas
                                        join p in context.tPacientes on c.idPaciente equals p.idPaciente
                                        join s in context.tSede on c.idSede equals s.idSede
                                        join cd in context.tCitasDisponibles on c.idCitaDisponible equals cd.idCitaDisponible
                                        where c.idPaciente == cita.idPaciente &&
                                              c.idSede == cita.idSede &&
                                              c.idCitaDisponible == cita.idCitaDisponible
                                        select new
                                        {
                                            NombrePaciente = p.Nombre,
                                            NombreSede = s.Nombre,
                                            FechaHora = cd.Fecha
                                        }).FirstOrDefault();

                        if (citaInfo != null)
                        {
                            // Cargar la plantilla del correo
                            string ruta = AppDomain.CurrentDomain.BaseDirectory + "correoCita.html";
                            string contenido = File.ReadAllText(ruta);

                            // Reemplazar los marcadores en la plantilla
                            contenido = contenido.Replace("@@NombrePaciente", citaInfo.NombrePaciente)
                                                 .Replace("@@FechaHora", citaInfo.FechaHora.ToString("dd/MM/yyyy HH:mm"))
                                                 .Replace("@@NombreSede", citaInfo.NombreSede);

                            // Enviar el correo
                            EnviarCorreoCitas("cherrera90114@ufide.ac.cr", contenido);
                        }

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error en un log
                throw new Exception("Hubo un problema al registrar la cita: " + ex.Message);
            }
        }


        public bool ReprogramarCita(Citas cita)
        {
            try
            {
                using (var context = new ProyectoGraduacionEntities())
                {
                    // Llamar al procedimiento almacenado para reprogramar la cita
                    var rowsAffected = context.ReprogramarCita(cita.idCita, cita.idPaciente, cita.idSede, cita.idCitaDisponible);

                    if (rowsAffected > 0)
                    {
                        // Recuperar la información de la cita reprogramada
                        var citaInfo = (from c in context.tCitas
                                        join p in context.tPacientes on c.idPaciente equals p.idPaciente
                                        join s in context.tSede on c.idSede equals s.idSede
                                        join cd in context.tCitasDisponibles on c.idCitaDisponible equals cd.idCitaDisponible
                                        where c.idCita == cita.idCita
                                        select new
                                        {
                                            NombrePaciente = p.Nombre,
                                            NombreSede = s.Nombre,
                                            FechaHora = cd.Fecha,
                                            CorreoPaciente = p.Correo
                                        }).FirstOrDefault();

                        if (citaInfo != null)
                        {
                            // Cargar la plantilla del correo
                            string ruta = AppDomain.CurrentDomain.BaseDirectory + "correoReprogramacion.html";
                            string contenido = File.ReadAllText(ruta);

                            // Reemplazar los marcadores
                            contenido = contenido.Replace("@@NombrePaciente", citaInfo.NombrePaciente)
                                                 .Replace("@@FechaHora", citaInfo.FechaHora.ToString("dd/MM/yyyy HH:mm"))
                                                 .Replace("@@NombreSede", citaInfo.NombreSede);

                            // Enviar el correo al paciente
                            EnviarCorreoCitas(citaInfo.CorreoPaciente, contenido);
                        }

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // Registrar error si es necesario
                throw new Exception("Hubo un problema al reprogramar la cita: " + ex.Message);
            }
        }




        public List<Citas> MisCitas(int idPaciente)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var query = from cita in context.tCitas
                            join sede in context.tSede on cita.idSede equals sede.idSede
                            join fecha in context.tCitasDisponibles on cita.idCitaDisponible equals fecha.idCitaDisponible
                            where cita.idPaciente == idPaciente
                            select new Citas
                            {
                                idCita = cita.idCita,                               
                                idSede = cita.idSede,
                                idCitaDisponible = cita.idCitaDisponible,
                                Fecha = fecha.Fecha, // Fecha de la cita
                                NombreSede = sede.Nombre // Nombre de la sede
                            };

                return query.ToList();
            }
        }

        public List<Citas> CitasProgramadas()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var query = from cita in context.tCitas
                            join paciente in context.tPacientes on cita.idPaciente equals paciente.idPaciente
                            join sede in context.tSede on cita.idSede equals sede.idSede
                            join fecha in context.tCitasDisponibles on cita.idCitaDisponible equals fecha.idCitaDisponible
                            orderby fecha.Fecha ascending

                            select new Citas
                            {
                                idCita = cita.idCita,
                                idPaciente = cita.idPaciente,
                                idSede = cita.idSede,
                                Fecha = fecha.Fecha, //Fecha de la cita
                                NombrePaciente = paciente.Nombre, // Nombre del paciente
                                NombreSede = sede.Nombre // Nombre de la sede
                            };

                return query.ToList();
            }
        }

        public bool CancelarCita(int idCita)
        {
            try
            {
                using (var context = new ProyectoGraduacionEntities())
                {
                    var cita = context.tCitas.FirstOrDefault(c => c.idCita == idCita);

                    if (cita != null)
                    {
                        // Recuperar la información necesaria de la cita cancelada
                        var citaInfo = (from c in context.tCitas
                                        join p in context.tPacientes on c.idPaciente equals p.idPaciente
                                        join s in context.tSede on c.idSede equals s.idSede
                                        join cd in context.tCitasDisponibles on c.idCitaDisponible equals cd.idCitaDisponible
                                        where c.idCita == idCita
                                        select new
                                        {
                                            NombrePaciente = p.Nombre,
                                            CorreoPaciente = p.Correo,
                                            NombreSede = s.Nombre,
                                            FechaHora = cd.Fecha
                                        }).FirstOrDefault();

                        if (citaInfo != null)
                        {
                            // Eliminar la cita
                            context.tCitas.Remove(cita);
                            context.SaveChanges();

                            // Cargar la plantilla del correo
                            string ruta = AppDomain.CurrentDomain.BaseDirectory + "correoCancelacion.html";
                            string contenido = File.ReadAllText(ruta);

                            // Reemplazar los marcadores en la plantilla
                            contenido = contenido.Replace("@@NombrePaciente", citaInfo.NombrePaciente)
                                                 .Replace("@@FechaHora", citaInfo.FechaHora.ToString("dd/MM/yyyy HH:mm"))
                                                 .Replace("@@NombreSede", citaInfo.NombreSede);

                            // Enviar el correo
                            EnviarCorreoCitas(citaInfo.CorreoPaciente, contenido);

                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error en un log si es necesario
                throw new Exception("Error al cancelar la cita: " + ex.Message);
            }
        }



        private void EnviarCorreoCitas(string destinatario, string contenido)
        {
            string cuenta = ConfigurationManager.AppSettings["CuentaCorreo"];
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaCorreo"];

            var message = new MailMessage
            {
                From = new MailAddress(cuenta),
                Subject = "Solicitud de cita",
                Body = contenido,
                Priority = MailPriority.Normal,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(destinatario));

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
                client.EnableSsl = true;
                client.Send(message);
            }
        }

        public bool EjecutarInsertarCitasDisponibles()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                context.insertar_citasDisponibles(); // No necesitas capturar retorno
                return true; // Siempre asumimos que insertó (porque el SP se encarga de evitar duplicados)
            }
        }




    }


}



