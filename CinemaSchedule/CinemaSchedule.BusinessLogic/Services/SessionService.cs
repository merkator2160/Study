using AutoMapper;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Models;
using CinemaSchedule.DataBase.Interfaces;
using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.BusinessLogic.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SessionService(IUniteOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // ISessionService ////////////////////////////////////////////////////////////////////////
        public IReadOnlyCollection<SessionDto> GetAll(Int32 cinemaId, Int32 filmId)
        {
            return _mapper.Map<IReadOnlyCollection<SessionDto>>(_unitOfWork.Sessions.GetAll(cinemaId, cinemaId));
        }
        public SessionDto Get(Int32 id)
        {
            return _mapper.Map<SessionDto>(_unitOfWork.Sessions.Get(id));
        }
        public void Update(SessionDto session)
        {
            var sessionDb = _unitOfWork.Sessions.Get(session.Id);
            var updatedSessionDb = _mapper.Map(session, sessionDb);
            _unitOfWork.Sessions.Update(updatedSessionDb);
            _unitOfWork.Commit();
        }
        public SessionDto Add(Int32 cinemaId, SessionDto session)
        {
            var cinemaDb = _unitOfWork.Cinemas.Get(cinemaId);
            var sessionDb = _mapper.Map<SessionDb>(session);
            cinemaDb.Sessions.Add(sessionDb);
            _unitOfWork.Commit();

            var sessionDto = _mapper.Map<SessionDto>(sessionDb);
            return sessionDto;
        }
        public void Delete(Int32 id)
        {
            _unitOfWork.Sessions.Delete(id);
            _unitOfWork.Commit();
        }
        public IReadOnlyDictionary<String, String> Validate(SessionDto session)
        {
            var errorDictionary = new Dictionary<String, String>();

            // here will be some of validation logic

            return errorDictionary;
        }
    }
}