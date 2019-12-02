using HhScraper.Models;
using HhScraper.Models.Config;
using HhScraper.Services.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HhScraper.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceConfig _config;


        public EmailService(ISettingsService settingsService)
        {
            _config = settingsService.GetConfig().EmailServiceConfig;
        }


        // IEmailService //////////////////////////////////////////////////////////////////////////
        public void Send(EmailMessage message)
        {
            using (var client = new SmtpClient(_config.SmtpUri, _config.Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(_config.Login, _config.Password),
                EnableSsl = _config.EnabledSsl
            })
            {
                client.Send(new MailMessage(_config.Login, message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                });
            }
        }
        public async Task SendAsync(EmailMessage message)
        {
            using (var client = new SmtpClient(_config.SmtpUri, _config.Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(_config.Login, _config.Password),
                EnableSsl = _config.EnabledSsl
            })
            {
                await client.SendMailAsync(new MailMessage(_config.Login, message.Destination)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                });
            }
        }
    }
}