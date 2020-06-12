using System.Net.Mail;
using AngularEshop.Core.Services.Interfaces;

namespace AngularEshop.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "mhrxadev@gmail.com";
            var mail = new MailMessage();
            var SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(defaultEmail, "Angular EShop");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "M96m96@75#75$");
            SmtpServer.EnableSsl = true;
            //SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Send(mail);
        }
    }
}

