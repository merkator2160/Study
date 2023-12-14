using Server.Services;
using System.Reflection;

namespace Server
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<RepeaterService>();
            app.MapGet("/", () => $"gRPC server v{Assembly.GetExecutingAssembly().GetName().Version}");

            app.Run();
        }
    }
}