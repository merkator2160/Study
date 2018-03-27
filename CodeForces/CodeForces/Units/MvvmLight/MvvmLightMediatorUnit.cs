using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace CodeForces.Units.MvvmLight
{
    public static class MvvmLightMediatorUnit
    {
        public static void Run()
        {
            var container = ConfigureContainer();
            UseMessenger(container);
        }
        private static IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            container.RegisterType<Handler>();

            return container;
        }
        private static void UseMessenger(IUnityContainer container)
        {
            var messanger = container.Resolve<IMessenger>();
            var handler = container.Resolve<Handler>();

            messanger.Send("Hello world!");
        }
    }
}