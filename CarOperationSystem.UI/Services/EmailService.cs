
using System.Net.Mail;
using System.Net;
using System.Text;
using CarOperationSystem.UI.Services.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using CarOperationSystem.UI.Configurations;
using Microsoft.Extensions.Options;

namespace CarOperationSystem.UI.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _emailSettings = settings.Value;
        }

        public async Task SendMail(string url, string toEmail)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_emailSettings.FromMail, _emailSettings.Password);

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.FromMail);

            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Reset password";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
    
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<a href={url}>Reset Application Password</a>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
