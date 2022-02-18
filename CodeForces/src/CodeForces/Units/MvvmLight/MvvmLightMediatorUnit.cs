using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using System;

namespace CodeForces.Units.MvvmLight
{
    public static class MvvmLightMediatorUnit
    {
        public static void Run()
        {
            var container = ConfigureContainer();
            UseMessenger(container);

            Console.ReadKey();
        }
        private static IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            container.RegisterType<Subscriber>();

            return container;
        }
        private static void UseMessenger(IUnityContainer container)
        {
            var messenger = container.Resolve<IMessenger>();
            var subscriber = container.Resolve<Subscriber>();

            messenger.Send("Hello world!");
        }
    }
}