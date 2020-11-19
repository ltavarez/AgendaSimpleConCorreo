using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailHandler
{
    public class EmailSender
    {

        public void SendEmail(string to,string subject,string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(EmailSetting.Default.SmtpServer);

                mail.From = new MailAddress(EmailSetting.Default.From);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.Port = EmailSetting.Default.Port;
                smtpServer.Credentials = new NetworkCredential(EmailSetting.Default.From, EmailSetting.Default.Paswword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("Se envio el correo");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

    }
}
