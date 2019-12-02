using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

namespace Katana.Components
{
    public static class TestComponentTwo
    {
        public static void UseTestComponentTwo(this IAppBuilder app)
        {
            app.Use(Handler);
        }
        private static async Task Handler(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("<h2>Test component 2</h2>");
            await next();
        }
    }
}