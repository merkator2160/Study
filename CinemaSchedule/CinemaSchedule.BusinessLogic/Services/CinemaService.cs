using AutoMapper;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Models;
using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.DataBase.Interfaces;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.BusinessLogic.Services
{
    public sealed class CinemaService : ICinemaService
    {
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _unitOfWork;


        public CinemaService(IUniteOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        // ICinemaService /////////////////////////////////////////////////////////////////
        public IReadOnlyCollection<CinemaDto> GetAll()
        {
            var cinemaDbs = _unitOfWork.Cinemas.GetAll();
            if (cinemaDbs != null)
            {
                var cinemaDtos = _mapper.Map<IReadOnlyCollection<CinemaDto>>(cinemaDbs);
                return cinemaDtos;
            }

            return new CinemaDto[] { };
        }
        public CinemaDto Get(Int32 id)
        {
            var cinemaDb = _unitOfWork.Cinemas.Get(id);
            if (cinemaDb != null)
            {
                var cinemaDto = _mapper.Map<CinemaDto>(cinemaDb);
                return cinemaDto;
            }

            throw new RequestedObjectNotFoundException($"Cinema with id = {id} not found");
        }
    }
}