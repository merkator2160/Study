using CodeForces.Units.MediatorNet.Messages;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading.Tasks;

namespace CodeForces.Units.MediatorNet.Handlers
{
    public class EventHandlerOne : IEventHandler<EventMessage>
    {
        public Task Handle(IReceiveContext<EventMessage> context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Event handler 1: {context.Message.Id}");
            });
        }
    }
}