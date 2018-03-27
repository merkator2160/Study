using System;
using System.Diagnostics;
using System.Threading;
using Unity.Wcf;


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
                var host = new UnityServiceHost(UnityFactory.GetInstance(), typeof(RequestService));

                Console.Write("Starting service ...  ");
                
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