using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

namespace Katana.Components
{
    public static class TestComponentOne
    {
        public static void UseTestComponentOne(this IAppBuilder app)
        {
            app.Use(Handler);
        }
        private static async Task Handler(IOwinContext context, Func<Task> next)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h2>Test component 1</h2>");

            await next();
        }
    }
}