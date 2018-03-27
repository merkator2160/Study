using AutoMapper;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Models;
using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.DataBase.Interfaces;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.BusinessLogic.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public FilmService(IUniteOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // IFilmService ///////////////////////////////////////////////////////////////////////////
        public IReadOnlyCollection<FilmDto> GetAll()
        {
            var filmDbs = _unitOfWork.Films.GetAll();
            if (filmDbs != null)
            {
                var filmDtos = _mapper.Map<IReadOnlyCollection<FilmDto>>(filmDbs);
                return filmDtos;
            }

            return new FilmDto[] { };
        }
        public FilmDto Get(Int32 id)
        {
            var filmDb = _unitOfWork.Films.Get(id);
            if (filmDb != null)
            {
                var filmDto = _mapper.Map<FilmDto>(filmDb);
                return filmDto;
            }

            throw new RequestedObjectNotFoundException($"Film with id = {id} not found");
        }
        public IReadOnlyCollection<FilmDto> GetByCinema(Int32 cinemaId)
        {
            var filmDbs = _unitOfWork.Films.GetByCinema(cinemaId);
            if (filmDbs != null)
            {
                var filmDtos = _mapper.Map<IReadOnlyCollection<FilmDto>>(filmDbs);
                return filmDtos;
            }

            return new FilmDto[] { };
        }
    }
}