using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CinemaScheduleV2
{
    public class Program
    {
        public static void Main(String[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(String[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}