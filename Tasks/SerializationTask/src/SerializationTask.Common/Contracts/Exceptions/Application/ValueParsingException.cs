using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class ValueParsingException : ApplicationException
    {
        public ValueParsingException()
        {

        }
        public ValueParsingException(String message) : base(message)
        {

        }
        public ValueParsingException(String message, Exception ex) : base(message)
        {

        }
        protected ValueParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}