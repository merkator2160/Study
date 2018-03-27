namespace HhScraper.Models.Enums
{
    public enum States
    {
        CollectingLinks,    // when page with links is already downlouded
        CollectingData,     // when all links are collected
        WatingForBrowser,   // waiting for browser opens
        Started,            // when starting
        Stopped             // when stopped
    }
}
