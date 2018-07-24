using AppointmentReminder.Models.Config;
using AppointmentReminder.Services.Interfaces;

namespace AppointmentReminder.Services
{
    public class SettingsService : ISettingsService
    {
        private RootConfig _config;


        // ISettingsService ///////////////////////////////////////////////////////////////////////
        public RootConfig GetConfig()
        {
            return _config ?? (_config = CreateDefault());
        }
        public void UpdateConfig(RootConfig config)
        {
            _config = config;
            SaveConfig();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private RootConfig CreateDefault()
        {
            return new RootConfig()
            {
                AppName = "Appointment reminder",
                IconName = "Email",
                TrayConfig = new TrayConfig()
                {
                    BalloonTitle = "Messenger",
                    BalloonLifetime = 500,
                },
                TwilioServiceConfig = new TwilioServiceConfig()
                {
                    AccountSid = "ACc2e889c3b92124cf74583ab907fddb43",
                    AuthToken = "7b1504b53ee83fde75409a18cf158c98",
                    TwilioNumber = "+18312285696"
                },
                EmailServiceConfig = new EmailServiceConfig()
                {
                    Login = "ulthaneTestReportSender@inbox.ru",
                    Password = "0ced6c89f0326f695db5ae512becd7b8",
                    EnabledSsl = true,
                    SmtpUri = "smtp.inbox.ru",
                    Port = 25
                }
            };
        }
        private void SaveConfig()
        {

        }
    }
}