using AppointmentReminder.Services;

namespace Sandbox.Units
{
    public static class TwilioServiceUnit
    {
        public static void Run()
        {
            var settingsService = new SettingsService();
            var twilioService = new TwilioService(settingsService);
            twilioService.SendSmsMessageAsync("+79875341715", "zxcvbn");
        }
    }
}