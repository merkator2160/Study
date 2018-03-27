using HhScraper.Models.Config;
using HhScraper.Services.Interfaces;

namespace HhScraper.Services
{
    public class SettingsService : ISettingsService
    {
        private RootConfig _config;


        // ISettingsService ///////////////////////////////////////////////////////////////////////
        public RootConfig GetConfig()
        {
            return _config ?? (_config = CreateDefault());
        }
        public void UpdateConfig(RootConfig config)
        {
            _config = config;
            SaveConfig();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private RootConfig CreateDefault()
        {
            return new RootConfig()
            {
                AppName = "HH Scraper",
                IconName = "Favicon",
                TrayConfig = new TrayConfig()
                {
                    BalloonTitle = "Messenger",
                    BalloonLifetime = 500,
                },
                EmailServiceConfig = new EmailServiceConfig()
                {
                    Login = "ulthaneTestReportSender@inbox.ru",
                    Password = "0ced6c89f0326f695db5ae512becd7b8",
                    EnabledSsl = true,
                    SmtpUri = "smtp.inbox.ru",
                    Port = 25
                },
                ScraperConfig = new ScraperConfig()
                {
                    HhVacancyPageUrlXpath = "//a[@class='output__name HH-VisitedResume-Href']",
                    HhVacancyPhotoUrlXpath = "//img[@class='noprint']",
                    HhVacancyAgeXpath = "//div[@class='resume__personal']/div[@class='resume__inlinelist']/span[@class='resume__inlinelist__item'][1]",
                    HhVacancyGenderXpath = "//div[@class='resume__personal']/div[@class='resume__inlinelist']/span[@class='resume__inlinelist__item'][2]",
                    HhVacancyAddressXpath = "//div[@class='resume__personal']/div[@class='resume__inlinelist']/span[@class='resume__inlinelist__item'][3]",
                    HhVacancyReadyToRelocateXpath = "//div[@class='resume__personal']/div[@class='resume__inlinelist']/span[@class='resume__inlinelist__item'][4]",
                    HhVacancyWorkExperienceXpath = "//div[@class='resume-block'][2]/div[@class='resume-block__title']/div[@class='resume-block__title__text']/span",
                    HhVacancySkillsXpath = "//div[@class='resume__twocols_cell']/div[2]",
                    HhVacancyPublicationDateXpath = "//span[@class='resume__updated']",
                    HhVacancyDesiredPositionXpath = "//div[@class='resume__position__title']",
                    HhVacancySalaryXpath = "//div[@class='resume__position__salary']",
                    HhVacancyFioXpath = "//div[@class='resume__personal__name']",
                    HhVacancyPhoneXpath = "//span[@class='resume__contacts__phone__number']",
                    HhVacancyFieldOfActivityXpath = "//div[@class='resume__position__specialization']",
                    HhVacancyWorkHistoryXpath = "//div[@class='resume__experience']",
                    HhVacancyEmailXpath = "//span[@class='resume__contacts__preferred g-round m-round_10']/span[1]/a",
                    HhVacancyEmailXpath2 = "//div[@class='resume__contacts']/div[@class='resume__inlinelist']/span[@class='resume__inlinelist__item']/span/span/a"
                }
            };
        }
        private void SaveConfig()
        {

        }
    }
}