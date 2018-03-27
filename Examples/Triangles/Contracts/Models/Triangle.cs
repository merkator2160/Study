using System;

namespace Contracts.Models
{
    public class Triangle
    {
        public Triangle()
        {
            Sides = new Double[] { 0, 0, 0 };

            Type = TriangleType.Undefined;
        }
        public Triangle(Double a, Double b, Double c)
        {
            Sides = new Double[] { a, b, c };
            Type = TriangleType.Undefined;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Double A
        {
            get { return Sides[0]; }
            set
            {
                Sides[0] = value;
            }
        }
        public Double B
        {
            get { return Sides[1]; }
            set
            {
                Sides[1] = value;
            }
        }
        public Double C
        {
            get { return Sides[2]; }
            set
            {
                Sides[2] = value;
            }
        }
        public TriangleType Type { get; set; }
        public Double[] Sides { get; }
    }
}