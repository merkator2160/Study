using System;

namespace CinemaSchedule.BusinessLogic.Models.Exceptions
{
    public class RequestedObjectNotFoundException : BusinessException
    {
        public RequestedObjectNotFoundException()
        {

        }
        public RequestedObjectNotFoundException(String message) : base(message)
        {

        }
        public RequestedObjectNotFoundException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }
}