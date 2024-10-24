using System;
using System.Configuration;
using System.Net.Mail;

namespace ProyectoDeGraduacion.Models
{
    public class GeneralModel
    {
        public void EnviarCorreo(string destino, string asunto, string contenido)
        {
            string cuenta = ConfigurationManager.AppSettings["CuentaCorreo"].ToString();
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaCorreo"].ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta);
            message.To.Add(new MailAddress(destino));
            message.Subject = asunto;
            message.Body = contenido;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}