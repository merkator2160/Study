using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using log4net.Config;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using RequestService.Common.Interfaces;
using RequestService.DataLayer;
using RequestService.Models;
using UnityLog4NetExtension.Log4Net;



namespace RequestService
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean createdNew;

            var multyStartupProtection = new Mutex(true, Properties.Settings.Default.MutexName, out createdNew);
            if (createdNew)
            {
                var container = new UnityContainer();

                //container.LoadConfiguration();
                DOMConfigurator.Configure();

                container.RegisterType<RequestService>();
                container.RegisterType<IDataProvider, DbDataProvider>();
                container.AddNewExtension<Log4NetExtension>();

                Console.Write("Starting service ...  ");

                var host = new RequestServiceHost(typeof(RequestService), container);
                try
                {
                    host.AddDefaultEndpoints();
                    host.Open();

                    Trace.WriteLine("Starting service, success");

                    Console.WriteLine("Done");
                    Console.WriteLine("Press any key to close the program.");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                finally
                {
                    host.Abort();
                }
            }
        }
    }
}