using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaSchedule.Models.ApiModels
{
    /// <summary>
    /// Entity which describe validation result or some kind of request processing error
    /// </summary>
    public class ErrorWrapperAm
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorWrapperAm()
        {
            CommonMessage = String.Empty;
            IsErrors = true;
            FormErrors = new ErrorAm[] { };
            FieldErrors = new ErrorAm[] { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isErrors"></param>
        public ErrorWrapperAm(Boolean isErrors)
        {
            CommonMessage = String.Empty;
            IsErrors = isErrors;
        }

        /// <summary>
        /// 
        /// </summary>
        public ErrorWrapperAm(String commonMessage)
        {
            CommonMessage = commonMessage;
            IsErrors = true;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Place for specific validation message or any other information
        /// </summary>
        [Required]
        public String CommonMessage { get; set; }

        /// <summary>
        /// Common validation flag. Indicates that some kind of error occured while processing the request
        /// </summary>
        [Required]
        public Boolean IsErrors { get; set; }

        /// <summary>
        /// Collection of form validation errors
        /// </summary>
        public IReadOnlyCollection<ErrorAm> FormErrors { get; set; }

        /// <summary>
        /// Collection of field validation errors
        /// </summary>
        public IReadOnlyCollection<ErrorAm> FieldErrors { get; set; }
    }
}