using AppointmentReminder.Forms;
using AppointmentReminder.Services;
using AppointmentReminder.Services.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Windows.Forms;
using UnityNLogExtension.NLog;

namespace AppointmentReminder.Core
{
    public class AppointmentReminderContext : ApplicationContext
    {
        private readonly IUnityContainer _container;


        public AppointmentReminderContext()
        {
            _container = ConfigureContainer();
            _container.Resolve<ITrayNotificator>();
            _container.Resolve<FormsManager>().ShowMain();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();

            container.AddNewExtension<NLogExtension<Logger>>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ITwilioService, TwilioService>();
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITrayNotificator, TrayNotificator>(new ContainerControlledLifetimeManager());
            container.RegisterType<FormsManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<Main>();
            container.RegisterType<Settings>();

            return container;
        }


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);
            _container.Dispose();
        }
    }
}