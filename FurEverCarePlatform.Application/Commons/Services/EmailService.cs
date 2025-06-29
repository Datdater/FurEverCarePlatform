using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailSettings = _configuration.GetSection("EmailSettings");
            var smtpClient = new SmtpClient
            {
                Host = mailSettings["Host"],
                Port = int.Parse(mailSettings["Port"]),
                EnableSsl = bool.Parse(mailSettings["EnableSsl"]),
                Credentials = new NetworkCredential(
                    mailSettings["Username"],
                    mailSettings["Password"]
                ),
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(mailSettings["FromEmail"], mailSettings["FromName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
