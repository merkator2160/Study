using CinemaSchedule.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace CinemaSchedule.BusinessLogic.Interfaces
{
    public interface ICinemaService
    {
        IReadOnlyCollection<CinemaDto> GetAll();
        CinemaDto Get(Int32 id);
    }
}