using CinemaSchedule.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.BusinessLogic.Interfaces
{
    public interface ISessionService
    {
        IReadOnlyCollection<SessionDto> GetAll(Int32 cinemaId, Int32 filmId);
        SessionDto Get(Int32 id);
        void Update(SessionDto session);
        SessionDto Add(Int32 cinemaId, SessionDto session);
        void Delete(Int32 id);
        IReadOnlyDictionary<String, String> Validate(SessionDto session);
    }
}