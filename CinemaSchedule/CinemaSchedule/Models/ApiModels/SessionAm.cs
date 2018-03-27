using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// Session entity
    /// </summary>
    public class SessionAm
    {
        /// <summary>
        /// Unique session entity identifier
        /// </summary>
        [Required]
        public Int32 Id { get; set; }

        /// <summary>
        /// The session begin time
        /// </summary>
        [Required]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// The current session film id
        /// </summary>
        [Required]
        public FilmAm Film { get; set; }
    }
}