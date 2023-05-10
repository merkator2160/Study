using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class RetryException : ApplicationException
    {
        public RetryException()
        {

        }
        public RetryException(String message) : base(message)
        {

        }
        public RetryException(String message, Exception ex) : base(message)
        {

        }
        protected RetryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}