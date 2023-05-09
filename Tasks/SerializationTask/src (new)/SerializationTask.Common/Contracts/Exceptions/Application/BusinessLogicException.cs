using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class BusinessLogicException : ApplicationException
    {
        public BusinessLogicException()
        {

        }
        public BusinessLogicException(String message) : base(message)
        {

        }
        public BusinessLogicException(String message, Exception ex) : base(message)
        {

        }
        protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}