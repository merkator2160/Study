using System;

namespace CinemaSchedule.BusinessLogic.Models.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }
        public BusinessException(String message) : base(message)
        {

        }
        public BusinessException(String message, Exception innerException) : base(message)
        {

        }
    }
}