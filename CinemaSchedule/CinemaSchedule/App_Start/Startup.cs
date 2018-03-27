using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CinemaSchedule.Startup))]
namespace CinemaSchedule
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}