using System;
using System.Collections.Generic;

namespace Contracts.Interfaces
{
    public interface ICalculator
    {
        IList<IOperation> GetOperations();
        double Operate(IOperation operation, Double[] operands);
    }
}