using Microsoft.Extensions.Options;
using MusicManager.Server.Core.Config;
using MusicManager.Server.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(string fromAddress, List<string> recipients, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<SmtpSettings> options)
        {
            _smtpSettings = options.Value;
            _smtpClient = InitializeSmtpClient();
        }

        public async Task SendEmail(string fromAddress, List<string> recipients, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);
                mail.Subject = subject;
                mail.Body = body;

                recipients.ForEach(recipient =>
                {
                    mail.To.Add(recipient); // TODO: Do we need this loop or can we do this in 1 step
                });

                await _smtpClient.SendMailAsync(mail);
            }
            catch (Exception)
            {
                // Ignore
            }
        }

        private SmtpClient InitializeSmtpClient()
        {
            SmtpClient client = new SmtpClient(_smtpSettings.SmtpServerAddress);
            client.Credentials = new NetworkCredential(_smtpSettings.SmtpUsername, _smtpSettings.SmtpPassword);
            client.Port = _smtpSettings.SmtpServerPort;
            client.EnableSsl = true;

            return client;
        }
    }
}
