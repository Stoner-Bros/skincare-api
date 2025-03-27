using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace APP.BLL.Implements
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
        Task SendEmail(string to, string emailType, Dictionary<string, string> placeholders);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!, to, subject, body)
            {
                IsBodyHtml = true // Set this property to true
            };
            await smtpClient.SendMailAsync(message);
        }

        public async Task SendEmail(string to, string emailType, Dictionary<string, string> placeholders)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates", $"{emailType}.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template '{emailType}' not found at path: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            var parsedTemplate = ParseTemplate(template, placeholders);

            var subject = placeholders.ContainsKey("Subject") ? placeholders["Subject"] : "No Subject";
            await SendEmail(to, subject, parsedTemplate);
        }

        private string ParseTemplate(string template, Dictionary<string, string> placeholders)
        {
            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }
            return template;
        }
    }
}
