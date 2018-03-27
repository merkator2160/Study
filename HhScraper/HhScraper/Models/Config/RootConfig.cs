using System;

namespace HhScraper.Models.Config
{
    public class RootConfig
    {
        public String IconName { get; set; }
        public String AppName { get; set; }
        public String ProgrammerEmail { get; set; }
        public TrayConfig TrayConfig { get; set; }
        public EmailServiceConfig EmailServiceConfig { get; set; }
        public ScraperConfig ScraperConfig { get; set; }
    }
}