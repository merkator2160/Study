﻿using System;

namespace AppointmentReminder.Models.Config
{
    public class EmailServiceConfig
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public Boolean EnabledSsl { get; set; }
        public String SmtpUri { get; set; }
        public Int32 Port { get; set; }
    }
}