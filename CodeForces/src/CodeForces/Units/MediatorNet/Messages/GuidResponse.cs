using Mediator.Net.Contracts;
using System;

namespace CodeForces.Units.MediatorNet.Messages
{
    public class GuidResponse : IResponse
    {
        public String ModifiedId { get; set; }
    }
}