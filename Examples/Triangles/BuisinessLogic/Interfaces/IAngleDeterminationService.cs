using BuisinessLogic.Models;
using System;

namespace BuisinessLogic.Interfaces
{
    public interface IAngleDeterminationService
    {
        String DetermineTriangleType(TriangleDto triangle);
    }
}