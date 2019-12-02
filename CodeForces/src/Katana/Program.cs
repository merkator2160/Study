using Microsoft.Owin.Hosting;
using System;

namespace Katana
{
    class Program
    {
        static void Main(String[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:3030"))
            {
                Console.WriteLine("Runing...");
                Console.ReadLine();
            }
        }
    }
}