using System;

namespace CinemaSchedule.DataBase.Models
{
    public class FilmDb
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public TimeSpan Length { get; set; }
    }
}