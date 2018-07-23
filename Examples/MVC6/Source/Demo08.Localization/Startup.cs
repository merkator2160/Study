using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Localization;

namespace Demo08.Localization
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddViewLocalization();
            services.AddTransient<IStringLocalizerFactory, ExampleStringLocalizerFactory>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestLocalization();
            app.UseMvcWithDefaultRoute();
        }
    }
}
