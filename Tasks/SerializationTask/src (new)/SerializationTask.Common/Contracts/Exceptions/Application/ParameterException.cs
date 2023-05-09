using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class ParameterException : ApplicationException
    {
        public ParameterException()
        {

        }
        public ParameterException(String message) : base(message)
        {

        }
        public ParameterException(String message, Exception ex) : base(message)
        {

        }
        protected ParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}