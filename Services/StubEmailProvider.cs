using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Numerics;

namespace GaryKAuthProject.Services
{
    public class StubEmailProvider : IEmailSender
    {
        IConfiguration Configuration { get; }
        private string FromEmailAddress;
        private string SmtpServerName;
        private int SmtpPort;

        public StubEmailProvider(IConfiguration configuration)
        {
            Configuration = configuration;
            FromEmailAddress = Configuration["FromEmailAddress"] ?? "from@test.com";
            SmtpServerName = Configuration["SmtpServerName"] ?? "localhost";
            string sSmtpPort = Configuration["SmtpPort"] ?? "25";
            SmtpPort = int.Parse(sSmtpPort);
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(FromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using (var client = new SmtpClient(SmtpServerName, SmtpPort))
            {
                client.Send(message);
            }
            
            return Task.CompletedTask;
        }
    }
}
