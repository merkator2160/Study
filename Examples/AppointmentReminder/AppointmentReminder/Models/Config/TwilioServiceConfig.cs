using System;

namespace AppointmentReminder.Models.Config
{
    public class TwilioServiceConfig
    {
        public String AccountSid { get; set; }
        public String AuthToken { get; set; }
        public String TwilioNumber { get; set; }
    }
}