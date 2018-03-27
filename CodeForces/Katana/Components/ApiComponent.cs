using Owin;

namespace Katana.Components
{
    public static class ApiComponent
    {
        public static void UseApiComponent(this IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Uri.AbsolutePath.StartsWith("/owin-api"))
                {
                    await context.Response.WriteAsync("Owin API");
                }
                await next();
            });
        }
    }
}