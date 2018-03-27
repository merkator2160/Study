using CinemaSchedule.DataBase.Interfaces;
using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CinemaSchedule.DataBase.Repositories
{
    public class FilmRepository : EfBaseRepository<FilmDb, DataContext>, IFilmRepository
    {
        public FilmRepository(DataContext context) : base(context)
        {

        }

        // ICinemaRepository //////////////////////////////////////////////////////////////////////
        public IReadOnlyCollection<FilmDb> GetByCinema(Int32 cinemaId)
        {
            var sessionWithFilmsByCInemaId = Context.Sessions.
                Include(p => p.Film).
                Where(p => p.Cinema.Id == cinemaId);
            var onlyFilms = sessionWithFilmsByCInemaId.Select(p => p.Film).ToArray();

            return onlyFilms;
        }
    }
}