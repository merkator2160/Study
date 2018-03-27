using AutoMapper;
using BuisinessLogic.Interfaces;
using BuisinessLogic.Services;
using Contracts.Interfaces;
using Microsoft.Practices.Unity;
using TriangleDeterminationExtService;

namespace BuisinessLogic.Infrastructure
{
    internal static class UnityConfig
    {
        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }
        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDetermineTriangleType, Determinator>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new InjectionParameter(0.001))
            );
            container.RegisterInstance(AutoMapperConfig.GetConfiguredMapper());
            container.RegisterType<IAngleDeterminationService, AngleService>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new InjectionParameter(container.Resolve<IDetermineTriangleType>()),
                    new InjectionParameter(container.Resolve<IMapper>()))
            );
        }
    }
}