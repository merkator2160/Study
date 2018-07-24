using AppointmentReminder.Models;
using System.Threading.Tasks;

namespace AppointmentReminder.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
        Task SendAsync(EmailMessage message);
    }
}