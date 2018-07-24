using AppointmentReminder.Models.Config;

namespace AppointmentReminder.Services.Interfaces
{
    public interface ISettingsService
    {
        RootConfig GetConfig();
        void UpdateConfig(RootConfig config);
    }
}