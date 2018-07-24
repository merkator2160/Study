using AppointmentReminder.Services.Interfaces;
using System;

namespace AppointmentReminder.Services
{
    public class TimeConverterService : ITimeConverterService
    {
        public DateTime ToLocalTime(DateTime time, String timezone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(time, TimeZoneInfo.FindSystemTimeZoneById(timezone)).ToLocalTime();
        }
    }
}