using System;

namespace AppointmentReminder.Services.Interfaces
{
    public interface ITimeConverterService
    {
        DateTime ToLocalTime(DateTime time, String timezone);
    }
}