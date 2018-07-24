using AppointmentReminder.Core;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AppointmentReminder
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (CheckAnyOtherInstances())
                return;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationOnThreadException;
            Application.ApplicationExit += OnApplicationExit;
            Application.Run(new AppointmentReminderContext());
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private static Boolean CheckAnyOtherInstances()
        {
            var guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

            Boolean created;
            var mutexObj = new Mutex(true, guid, out created);
            if (!created)
                return true;
            return false;
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private static void OnApplicationExit(Object sender, EventArgs eventArgs)
        {

        }
        private static void CurrentDomainOnUnhandledException(Object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {

        }
        private static void ApplicationOnThreadException(Object sender, ThreadExceptionEventArgs threadExceptionEventArgs)
        {

        }
    }
}
