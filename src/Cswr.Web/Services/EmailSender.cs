using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Cswr.Web.Services;

    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "brad@fornitsweb.com";
            string fromPassword = "epwouajldpcobuym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress("accounts@cheatsheetwarroom.com");
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            return Task.CompletedTask;
        }
    }
