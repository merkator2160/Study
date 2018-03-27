using Contracts;
using Contracts.Interfaces;
using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleDeterminationExtService;

namespace Triangles.Tests
{
    [TestClass]
    public class DeterminatorTests
    {
        private readonly IDetermineTriangleType _determineTrangleType;


        public DeterminatorTests()
        {
            _determineTrangleType = new Determinator(0.001);
        }


        // TESTS //////////////////////////////////////////////////////////////////////////////////
        [TestMethod]
        public void AcuteTest()
        {
            var triangle = new Triangle()
            {
                A = 15.6,
                B = 12.4,
                C = 3.9
            };
            _determineTrangleType.DetermineType(ref triangle);
            Assert.AreEqual(TriangleType.Acute, triangle.Type);
        }

        [TestMethod]
        public void ObtuseTest()
        {
            var triangle = new Triangle()
            {
                A = 25.9,
                B = 28.2,
                C = 27.7
            };
            _determineTrangleType.DetermineType(ref triangle);
            Assert.AreEqual(TriangleType.Obtuse, triangle.Type);
        }

        [TestMethod]
        public void RectangularTest()
        {
            var triangle = new Triangle()
            {
                A = 6,
                B = 8,
                C = 10
            };
            _determineTrangleType.DetermineType(ref triangle);
            Assert.AreEqual(TriangleType.Rectangular, triangle.Type);
        }

        [TestMethod]
        public void EquilateralTest()
        {
            var triangle = new Triangle()
            {
                A = 5,
                B = 5,
                C = 5
            };
            _determineTrangleType.DetermineType(ref triangle);
            Assert.AreEqual(TriangleType.Equilateral, triangle.Type);
        }

        [TestMethod]
        public void IsoscelesTest()
        {
            var triangle = new Triangle()
            {
                A = 25.9,
                B = 25.9,
                C = 18.4
            };
            _determineTrangleType.DetermineType(ref triangle);
            Assert.AreEqual(TriangleType.Isosceles, triangle.Type);
        }
    }
}
