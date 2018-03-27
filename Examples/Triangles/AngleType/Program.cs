using BuisinessLogic.Infrastructure;
using BuisinessLogic.Models;
using System;

namespace Triangles
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var services = new ServiceLocator())
            {
                try
                {
                    var triangles = GetTriangles();
                    foreach (var x in triangles)
                    {
                        Console.WriteLine($"A:{x.A}; B:{x.B}; C:{x.C}");
                        Console.WriteLine(services.AngleService.DetermineTriangleType(x));
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadKey();
        }
        private static TriangleDto[] GetTriangles()
        {
            return new TriangleDto[]
            {
                new TriangleDto()
                {
                    A = 15.6,
                    B = 12.4,
                    C = 3.9
                },
                new TriangleDto()
                {
                    A = 25.9,
                    B = 25.9,
                    C = 18.4
                },
                new TriangleDto()
                {
                    A = 25.9,
                    B = 28.2,
                    C = 27.7
                },
                new TriangleDto()
                {
                    A = 6,
                    B = 8,
                    C = 10
                },
                new TriangleDto()
                {
                    A = 5,
                    B = 5,
                    C = 5
                }
            };
        }
    }
}
