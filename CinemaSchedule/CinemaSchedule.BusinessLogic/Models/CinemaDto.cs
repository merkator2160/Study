using System;

namespace CinemaSchedule.BusinessLogic.Models
{
    public class CinemaDto
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public SessionDto[] Sessions { get; set; }
    }
}