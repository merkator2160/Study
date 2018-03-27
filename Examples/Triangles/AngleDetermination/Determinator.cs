using Contracts;
using Contracts.Interfaces;
using Contracts.Models;
using System;
using System.Linq;

namespace TriangleDeterminationExtService
{
    public class Determinator : IDetermineTriangleType
    {
        private readonly Double _determinationAccuracy;


        public Determinator(Double determinationAccuracy)
        {
            _determinationAccuracy = determinationAccuracy;
        }


        // IDetermineTriangleType ////////////////////////////////////////////////////////////////////////
        void IDetermineTriangleType.DetermineType(ref Triangle triangle)
        {
            if (Math.Abs(triangle.A - triangle.B) < _determinationAccuracy &&
                Math.Abs(triangle.B - triangle.C) < _determinationAccuracy)
            {
                triangle.Type = TriangleType.Equilateral;
                return;
            }

            if (Math.Abs(triangle.A - triangle.B) < _determinationAccuracy ||
                Math.Abs(triangle.B - triangle.C) < _determinationAccuracy ||
                Math.Abs(triangle.C - triangle.A) < _determinationAccuracy)
            {
                triangle.Type = TriangleType.Isosceles;
                return;
            }

            var orderedSides = triangle.Sides.OrderBy(p => p).ToArray();
            if (Math.Abs(Math.Pow(orderedSides[2], 2) - (Math.Pow(orderedSides[0], 2) + Math.Pow(orderedSides[1], 2))) < _determinationAccuracy)
            {
                triangle.Type = TriangleType.Rectangular;
                return;
            }

            if (Math.Pow(orderedSides[0], 2) + Math.Pow(orderedSides[1], 2) < Math.Pow(orderedSides[2], 2))
            {
                triangle.Type = TriangleType.Acute;
            }
            else
            {
                triangle.Type = TriangleType.Obtuse;
            }
        }
    }
}
