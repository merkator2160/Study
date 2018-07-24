using System;

namespace AppointmentReminder.Models.Config
{
    public class RootConfig
    {
        public String IconName { get; set; }
        public String AppName { get; set; }
        public TrayConfig TrayConfig { get; set; }
        public TwilioServiceConfig TwilioServiceConfig { get; set; }
        public EmailServiceConfig EmailServiceConfig { get; set; }
    }
}