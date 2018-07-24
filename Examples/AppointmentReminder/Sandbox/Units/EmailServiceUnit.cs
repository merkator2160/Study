using AppointmentReminder.Models;
using AppointmentReminder.Services;

namespace Sandbox.Units
{
    public static class EmailServiceUnit
    {
        public static void Run()
        {
            var settingsService = new SettingsService();
            var emailService = new EmailService(settingsService);
            emailService.Send(new EmailMessage()
            {
                Body = "Test.",
                Destination = "2160@inbox.ru",
                Subject = "Appointment reminder test"
            });
        }
    }
}