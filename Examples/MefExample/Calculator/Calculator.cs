using Calculator.Models;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Calculator
{
    [Export(typeof(ICalculator))]
    public class Calculator : ICalculator
    {
        public IList<IOperation> GetOperations()
        {
            return new List<IOperation>()
            {
                new Operation { Name = "+", NumberOperands = 2},
                new Operation { Name = "-", NumberOperands = 2},
                new Operation { Name = "/", NumberOperands = 2},
                new Operation { Name = "*", NumberOperands = 2}
            };
        }

        public double Operate(IOperation operation, Double[] operands)
        {
            Double result;
            switch (operation.Name)
            {
                case "+":
                    result = operands[0] + operands[1];
                    break;
                case "-":
                    result = operands[0] - operands[1];
                    break;
                case "/":
                    result = operands[0] / operands[1];
                    break;
                case "*":
                    result = operands[0] * operands[1];
                    break;
                default:
                    throw new InvalidOperationException($"Некорректная операция {operation.Name}");
            }
            return result;
        }
    }
}