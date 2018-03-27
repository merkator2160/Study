using System;

namespace CinemaSchedule.DataBase.Models
{
    public class SessionDb
    {
        public Int32 Id { get; set; }
        public DateTime BeginDate { get; set; }
        public virtual FilmDb Film { get; set; }
        public virtual CinemaDb Cinema { get; set; }
    }
}