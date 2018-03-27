using Mediator.Net.Contracts;
using System;

namespace CodeForces.Units.MediatorNet.Messages
{
    public class GuidCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}