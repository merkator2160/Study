using CodeForces.Units.Semafore.Models;
using System;

namespace CodeForces.Units.Semafore
{
    public static class SemaforeUnite
    {
        public static void Run()
        {
            for (var i = 1; i < 6; i++)
            {
                var reader = new Reader(i);
            }

            Console.ReadKey();
        }
    }
}