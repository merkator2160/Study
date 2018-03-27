using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.DataBase.Interfaces
{
    public interface ISessionRepository : IRepository<SessionDb>
    {
        IReadOnlyCollection<SessionDb> GetAll(Int32 cinemaId, Int32 filmId);
    }
}