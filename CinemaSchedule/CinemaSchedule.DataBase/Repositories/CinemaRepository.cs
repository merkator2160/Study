using CinemaSchedule.DataBase.Interfaces;
using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CinemaSchedule.DataBase.Repositories
{
    public class CinemaRepository : EfBaseRepository<CinemaDb, DataContext>, ICinemaRepository
    {
        public CinemaRepository(DataContext context) : base(context)
        {

        }


        // IRepository<CinemaDb> //////////////////////////////////////////////////////////////////
        public override IReadOnlyCollection<CinemaDb> GetAll()
        {
            return Context.Cinemas.
                Include(p => p.Sessions.Select(k => k.Film)).
                ToArray();
        }
        public override CinemaDb Get(Int32 id)
        {
            return Context.Cinemas.
                Include(p => p.Sessions.Select(k => k.Film)).
                FirstOrDefault(p => p.Id == id);
        }
    }
}