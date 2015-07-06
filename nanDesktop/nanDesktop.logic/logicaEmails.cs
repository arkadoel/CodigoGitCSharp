/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 09/27/2013
 * Hora: 10:38
 * 
 * 
 */
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using FeedBackManager;

namespace nanDesktop.logic
{
	/// <summary>
	/// Description of logicaEmails.
	/// </summary>
	public class logicaEmails
	{
		public const string EMAIL_NAN = "contacto@notanumber.org.es";
		
		 /// <summary>
        /// Mandar email con un adjunto
        /// </summary>
        /// <param name="_rutaIMG"></param>
        /// <param name="_texto"></param>
        /// <param name="_asunto"></param>
        /// <param name="_destinatario"></param>
        public static Boolean mandarEmailConAdjunto(String _archivoAdjunto, String _texto, String _asunto, String _destinatario)
        {
            try
            {
                // Specify the file to be attached and sent.
                // This example assumes that a file named Data.xls exists in the
                // current working directory.
                string file = _archivoAdjunto;
                // Create a message and set up the recipients.
                MailMessage message = new MailMessage(
                   _destinatario,
                   _destinatario,
                   _asunto,
                   _texto);

                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                /*ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                */
                // Add the file attachment to this e-mail message.
                message.Attachments.Add(data);

                //Send the message.
                SmtpClient smtpcli = new SmtpClient("smtp.gmail.com", 587); //use this PORT!
                smtpcli.EnableSsl = true;
                smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpcli.Credentials = new NetworkCredential("mipafedevelop@gmail.com", "ParaEmails");

                smtpcli.Send(message);

                data.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Logs.WriteText("Error envial email (Datos correo)", _texto);
                Logs.WriteError("Error al enviar email", ex);
                return false;
            }            
        }

        /// <summary>
        /// Enviar email ordinario, sin archivos adjuntos
        /// </summary>
        /// <param name="_texto"></param>
        /// <param name="_asunto"></param>
        /// <param name="_destinatario"></param>
        public static void mandarEmail(String _texto, String _asunto, String _destinatario)
        {        try{    
                    MailMessage message = new MailMessage(
                       _destinatario,
                       _destinatario,
                       _asunto,
                       _texto);

                    //enviar
                    SmtpClient smtpcli = new SmtpClient("smtp.gmail.com", 587); //use this PORT!
                    smtpcli.EnableSsl = true;
                    smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpcli.Credentials = new NetworkCredential("mipafedevelop@gmail.com", "ParaEmails");

                    smtpcli.Send(message);
                }
                catch(Exception ex)
                {
                    Logs.WriteText("Error envial email (Datos correo)", _texto);
                    Logs.WriteError("Error al enviar email", ex);
                }
        }

        public static void emailForMe(string texto, string asunto)
        {
            try
            {
                String _texto = texto;
                String _asunto = asunto;
                String _destinatario = EMAIL_NAN;
                MailMessage message = new MailMessage(
                   _destinatario,
                   _destinatario,
                   _asunto,
                   _texto);

                //enviar
                SmtpClient smtpcli = new SmtpClient("smtp.gmail.com", 587); //use this PORT!
                smtpcli.EnableSsl = true;
                smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpcli.Credentials = new NetworkCredential("mipafedevelop@gmail.com", "ParaEmails");

                smtpcli.Send(message);
                MessageBox.Show("Mensaje enviado correctamente.", "Enviado",MessageBoxButton.OK,MessageBoxImage.Information);
            
            }
            catch(Exception ex) {
                Logs.WriteText("Error envial email (Datos correo)", texto);
                Logs.WriteError("Error al enviar email", ex);
            }
        }

        public static Boolean emailForMeConAdjunto(string texto, string asunto, string _adjunto)
        {
            return mandarEmailConAdjunto(_adjunto, texto, asunto, EMAIL_NAN);
        }

	}
}
