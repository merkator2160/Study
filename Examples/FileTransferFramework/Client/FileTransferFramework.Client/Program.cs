
using System;
using System.Runtime.InteropServices;
using System.ServiceModel;
namespace FileTransferFramework.Client
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        static void Main(string[] args)
        {
            try
            {
                //IntPtr hwnd;
                //hwnd = GetConsoleWindow();
                //ShowWindow(hwnd, SW_HIDE);

                using (var serviceHost = new ServiceHost(typeof(FileTransfer)))
                {
                    serviceHost.AddDefaultEndpoints();
                    serviceHost.Open();
                    Console.WriteLine("Client Started..");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}
