using System;

namespace AppointmentReminder.Core
{
    public interface ITrayNotificator
    {
        void ShowMessage(String message);
        void ShowError(String message);
    }
}