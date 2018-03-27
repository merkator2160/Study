using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CinemaScheduleV2.Middleware
{
    internal static class MvcMiddleware
    {
        public static IApplicationBuilder UseConfiguredMvc(this IApplicationBuilder app)
        {
            app.UseMvc(ConfigureRotes);

            return app;
        }
        public static void ConfigureRotes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

            routeBuilder.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}