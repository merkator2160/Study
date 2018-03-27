using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// Cinema entity
    /// </summary>
    public class CinemaAm
    {
        /// <summary>
        /// Unique cinema entity identifier
        /// </summary>
        [Required]
        public Int32 Id { get; set; }

        /// <summary>
        /// The name of the cinema
        /// </summary>
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// Collection of the current cinema sessions
        /// </summary>
        public SessionAm[] Sessions { get; set; }
    }
}