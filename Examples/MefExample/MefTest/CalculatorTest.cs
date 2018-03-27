using Contracts.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MefTest
{
    public class CalculatorTest
    {
        public CalculatorTest(String assmblyPath)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(assmblyPath));
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        [Import]
        public ICalculator Calculator { get; set; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            var ops = Calculator.GetOperations();
            Console.WriteLine(Calculator.Operate(ops[0], new Double[] { 3.7, 19.34 }));
            Console.WriteLine(Calculator.Operate(ops[1], new Double[] { 3.7, 19.34 }));
            Console.WriteLine(Calculator.Operate(ops[2], new Double[] { 3.7, 19.34 }));
            Console.WriteLine(Calculator.Operate(ops[3], new Double[] { 3.7, 19.34 }));
        }
    }
}