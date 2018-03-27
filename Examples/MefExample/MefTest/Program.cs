using System;
using System.IO;
using System.Reflection;

namespace MefTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var assemblyPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            try
            {
                //new StringTransformer(assemblyPath).Run();
                new CalculatorTest(assemblyPath).Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
