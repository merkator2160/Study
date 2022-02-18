using CodeForces.Units.MediatorNet.Messages;
using Mediator.Net;
using Mediator.Net.Unity;
using Microsoft.Practices.Unity;
using System;

namespace CodeForces.Units.MediatorNet
{
    public static class MediatorNetUnit
    {
        public static void Run()
        {
            var container = ConfigureContainer();
            RunMediator(container.Resolve<IMediator>());
        }
        private static IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();

            var mediaBuilder = new MediatorBuilder();
            mediaBuilder.RegisterHandlers(typeof(MediatorNetUnit).Assembly);
            UnityExtensioins.Configure(mediaBuilder, container);

            return container;
        }
        private static async void RunMediator(IMediator mediator)
        {
            await mediator.PublishAsync(new EventMessage
            {
                Id = Guid.NewGuid()
            });
            await mediator.SendAsync(new GuidCommand
            {
                Id = Guid.NewGuid()
            });
            var result = await mediator.RequestAsync<GuidRequest, GuidResponse>(new GuidRequest
            {
                Id = Guid.NewGuid().ToString()
            });
            Console.WriteLine($"Request handler: {result.ModifiedId}");
        }
    }
}