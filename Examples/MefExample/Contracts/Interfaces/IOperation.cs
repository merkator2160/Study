using System;

namespace Contracts.Interfaces
{
    public interface IOperation
    {
        String Name { get; }
        Int32 NumberOperands { get; }
    }
}