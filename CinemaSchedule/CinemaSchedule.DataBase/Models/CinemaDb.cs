using System;
using System.Collections.Generic;

namespace CinemaSchedule.DataBase.Models
{
    public class CinemaDb
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public virtual ICollection<SessionDb> Sessions { get; set; }
    }
}