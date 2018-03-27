using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// FIlm entity
    /// </summary>
    public class FilmAm
    {
        /// <summary>
        /// Unique film entity identifier
        /// </summary>
        [Required]
        public Int32 Id { get; set; }

        /// <summary>
        /// The name of the film
        /// </summary>
        [Required]
        public String Name { get; set; }
    }
}