using System;
using System.Threading.Tasks;

namespace AppointmentReminder.Services.Interfaces
{
    public interface ITwilioService
    {
        Task SendSmsMessageAsync(String phoneNumber, String message);
    }
}