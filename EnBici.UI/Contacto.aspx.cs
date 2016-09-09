using Recaptcha.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnBici
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Método para limpiar el formulario
        /// </summary>
        private void Limpiar()
        {
            txtTitulo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtMensaje.Text = string.Empty;
            lblAviso.Text = string.Empty;
        }

        /// <summary>
        /// Método para enviar el correo electrónico
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <param name="mensaje"></param>
        private void Enviar(string titulo, string nombre, string correo, string mensaje)
        {
            if (String.IsNullOrEmpty(Recaptcha1.Response))
            {
                lblAviso.Text = "El código captcha no puede estar vacío.";
            }
            else
            {
                RecaptchaVerificationResult result = Recaptcha1.Verify();

                if (result == RecaptchaVerificationResult.Success)
                {
                    string to = ConfigurationManager.AppSettings["To"];
                    MailMessage mail = new MailMessage();
                    mail.To.Add(new MailAddress(to));
                    mail.From = new MailAddress(correo, nombre);
                    mail.Subject = titulo + " " + correo;
                    mail.Body = mensaje + "\n\n" + nombre + "\n" + correo;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Send(mail);
                    Limpiar();
                    lblAviso.Text = "Mensaje enviado. Gracias por su tiempo.";

                }
                else
                {
                    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
                    {
                        lblAviso.Text = "El códifo de verificacion es incorrecto.";
                    }
                    else
                    {
                        lblAviso.Text = "Ha ocurrido un error al reconocer el código captcha.";
                    }
                }
            }                       
        }

        /// <summary>
        /// Método del evento del botón para enviar el contenido del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Enviar(txtTitulo.Text, txtNombre.Text, txtCorreo.Text, txtMensaje.Text);
        }
    }   
}