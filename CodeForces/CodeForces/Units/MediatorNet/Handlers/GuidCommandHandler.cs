using CodeForces.Units.MediatorNet.Messages;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading.Tasks;

namespace CodeForces.Units.MediatorNet.Handlers
{
    public class GuidCommandHandler : ICommandHandler<GuidCommand>
    {
        public Task Handle(ReceiveContext<GuidCommand> context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Command handler: {context.Message.Id}");
            });
        }
    }
}