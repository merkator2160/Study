using CinemaSchedule.DataBase.Interfaces;
using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaSchedule.DataBase.Repositories
{
    public class SessionRepository : EfBaseRepository<SessionDb, DataContext>, ISessionRepository
    {
        public SessionRepository(DataContext context) : base(context)
        {

        }


        // ISessionRepository /////////////////////////////////////////////////////////////////////
        public IReadOnlyCollection<SessionDb> GetAll(Int32 cinemaId, Int32 filmId)
        {
            return Context.Sessions.Where(p => p.Cinema.Id == cinemaId && p.Film.Id == filmId).ToArray();
        }
    }
}