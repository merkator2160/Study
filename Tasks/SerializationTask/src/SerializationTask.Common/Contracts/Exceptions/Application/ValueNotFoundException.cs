using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class ValueNotFoundException : ApplicationException
    {
        public ValueNotFoundException()
        {

        }
        public ValueNotFoundException(String message) : base(message)
        {

        }
        public ValueNotFoundException(String message, Exception ex) : base(message)
        {

        }
        protected ValueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}