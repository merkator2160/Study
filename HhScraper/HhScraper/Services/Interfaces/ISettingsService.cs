using HhScraper.Models.Config;

namespace HhScraper.Services.Interfaces
{
    public interface ISettingsService
    {
        RootConfig GetConfig();
        void UpdateConfig(RootConfig config);
    }
}