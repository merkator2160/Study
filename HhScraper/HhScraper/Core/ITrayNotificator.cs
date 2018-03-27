using System;

namespace HhScraper.Core
{
    public interface ITrayNotificator
    {
        void ShowMessage(String message);
        void ShowError(String message);
    }
}