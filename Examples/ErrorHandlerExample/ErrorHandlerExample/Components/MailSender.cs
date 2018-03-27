using System.Net;
using System.Net.Mail;
using ErrorHandlerExample.Properties;

namespace ErrorHandlerExample.Components
{
    public static class MailSender
    {


        public static void SendEmail(string to, string copy, string from, string boby, string subject, int entityId, string entityType)
        {
            var client = new SmtpClient(Settings.Default.ServerSMTP, Settings.Default.PortSMTP)
            {
                Credentials = new NetworkCredential(Settings.Default.ServerSMTP, Settings.Default.PasswdSMTP)
            };

            client.EnableSsl = false;
            var message = new MailMessage(from, to, subject, boby);

            if(!string.IsNullOrEmpty(copy))
                message.CC.Add(copy);

            client.Send(message);
          
        }
    }
}
