using Mediator.Net.Contracts;
using System;

namespace CodeForces.Units.MediatorNet.Messages
{
    public class EventMessage : IEvent
    {
        public Guid Id { get; set; }
    }
}