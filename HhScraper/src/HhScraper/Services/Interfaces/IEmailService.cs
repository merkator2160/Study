using HhScraper.Models;
using System.Threading.Tasks;

namespace HhScraper.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
        Task SendAsync(EmailMessage message);
    }
}