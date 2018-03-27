using Mediator.Net.Contracts;
using System;

namespace CodeForces.Units.MediatorNet.Messages
{
    public class GuidRequest : IRequest
    {
        public String Id { get; set; }
    }
}