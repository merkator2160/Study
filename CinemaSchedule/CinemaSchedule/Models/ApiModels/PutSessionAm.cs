using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// Put session model
    /// </summary>
    public class PutSessionAm
    {
        /// <summary>
        /// BeginDate 
        /// </summary>
        [Required(ErrorMessage = "The field must be set")]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Film id
        /// </summary>
        [Required(ErrorMessage = "The field must be set")]
        public Int32 FilmId { get; set; }
    }
}