using CinemaSchedule.DataBase.Models;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.DataBase.Interfaces
{
    public interface IFilmRepository : IRepository<FilmDb>
    {
        IReadOnlyCollection<FilmDb> GetByCinema(Int32 cinemaId);
    }
}