using AutoMapper;
using BuisinessLogic.Interfaces;
using BuisinessLogic.Models;
using Contracts;
using Contracts.Interfaces;
using Contracts.Models;
using System;

namespace BuisinessLogic.Services
{
    public class AngleService : IAngleDeterminationService
    {
        private readonly IDetermineTriangleType _determineTrangleType;
        private readonly IMapper _mapper;


        public AngleService(IDetermineTriangleType determineTrangleType, IMapper mapper)
        {
            _mapper = mapper;
            _determineTrangleType = determineTrangleType;
        }


        // IAngleDeterminationService /////////////////////////////////////////////////////////////
        String IAngleDeterminationService.DetermineTriangleType(TriangleDto triangle)
        {
            var triangleWithType = _mapper.Map<Triangle>(triangle);
            _determineTrangleType.DetermineType(ref triangleWithType);

            switch (triangleWithType.Type)
            {
                case TriangleType.Acute:
                    return "острый";

                case TriangleType.Obtuse:
                    return "тупой";

                case TriangleType.Rectangular:
                    return "прямоугольный";

                case TriangleType.Equilateral:
                    return "равносторонний";

                case TriangleType.Isosceles:
                    return "равнобедренный";

                default:
                    throw new InvalidOperationException("Undefined triangle type");
            }
        }
    }
}