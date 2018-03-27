using System;
using System.Collections.Generic;

namespace CodeForces.Units
{
    public static class YeldUnit
    {
        public static void Run()
        {
            // Display powers of 2 up to the exponent of 8:
            Console.WriteLine(String.Join(" ", Power(2, 8)));
            Console.ReadKey();
        }
        public static IEnumerable<Int32> Power(Int32 number, Int32 exponent)
        {
            var result = 1;

            for (var i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
    }
}