using Contracts.Interfaces;
using System;

namespace Calculator.Models
{
    public class Operation : IOperation
    {
        public String Name { get; internal set; }
        public Int32 NumberOperands { get; internal set; }
    }
}