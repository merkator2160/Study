using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Application
{
    public class BusinessLogicValidationException : ApplicationException
    {
        public BusinessLogicValidationException()
        {

        }
        public BusinessLogicValidationException(String message) : base(message)
        {

        }
        public BusinessLogicValidationException(String message, Exception ex) : base(message)
        {

        }
        protected BusinessLogicValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}