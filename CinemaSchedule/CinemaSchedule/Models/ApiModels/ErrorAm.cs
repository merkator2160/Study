using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// Validation error entity
    /// </summary>
    public class ErrorAm
    {
        /// <summary>
        /// Name of incorrect field
        /// </summary>
        [Required]
        public String FieldName { get; set; }

        /// <summary>
        /// Reason
        /// </summary>
        [Required]
        public String Message { get; set; }
    }
}