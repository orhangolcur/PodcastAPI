using Microsoft.Extensions.Configuration;
using PodcastAPI.Application.Abstractions;
using System.Net.Mail;

namespace PodcastAPI.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpHost = _configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"] ?? "25");
            var fromEmail = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
