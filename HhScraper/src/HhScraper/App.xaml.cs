using GalaSoft.MvvmLight.Messaging;
using HhScraper.Core;
using HhScraper.Services;
using HhScraper.Services.Interfaces;
using HhScraper.ViewModels;
using HhScraper.Views;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using UnityNLogExtension.NLog;

namespace HhScraper
{
    public partial class App
    {
        private readonly IUnityContainer _container;


        public App()
        {
            _container = ConfigureContainer();

            Current.Startup += CurrentOnStartup;
            Current.Exit += CurrentOnExit;
            Current.SessionEnding += CurrentOnSessionEnding;
            Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void CurrentOnStartup(Object sender, StartupEventArgs startupEventArgs)
        {
            if (CheckAnyOtherInstances())
                Shutdown();

            _container.Resolve<ITrayNotificator>();
            _container.Resolve<FormsManager>().ShowMain();
        }
        private void CurrentOnExit(Object sender, ExitEventArgs exitEventArgs)
        {
            _container?.Dispose();
        }
        private void CurrentOnDispatcherUnhandledException(Object sender, DispatcherUnhandledExceptionEventArgs ea)
        {
            _container.Resolve<ErrorHandlerVm>().Run(ea.Exception);
        }
        private void CurrentOnSessionEnding(Object sender, SessionEndingCancelEventArgs sessionEndingCancelEventArgs)
        {

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
        private IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();

            container.AddNewExtension<NLogExtension<Logger>>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITrayNotificator, TrayNotificator>(new ContainerControlledLifetimeManager());
            container.RegisterType<FormsManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<ErrorHandlerVm>();
            container.RegisterType<Scraper>();
            container.RegisterType<Settings>();
            container.RegisterType<FancyPopup>();
            container.RegisterType<ErrorHandlerForm>();

            return container;
        }
    }
}