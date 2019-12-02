using CodeForces.Units.MediatorNet.Messages;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading.Tasks;

namespace CodeForces.Units.MediatorNet.Handlers
{
    public class RequestHandler : IRequestHandler<GuidRequest, GuidResponse>
    {
        public Task<GuidResponse> Handle(ReceiveContext<GuidRequest> context)
        {
            return Task.Run(() => new GuidResponse
            {
                ModyfiedId = $"{context.Message.Id} - from response"
            });
        }
    }
}