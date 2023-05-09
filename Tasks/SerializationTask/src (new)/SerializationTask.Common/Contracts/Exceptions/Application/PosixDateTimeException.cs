using System.Runtime.Serialization;

namespace SerializationTask.Common.Contracts.Exceptions.Application
{
    public class PosixDateTimeException : ApplicationException
    {
        public PosixDateTimeException()
        {

        }
        public PosixDateTimeException(String message) : base(message)
        {

        }
        public PosixDateTimeException(String message, Exception ex) : base(message)
        {

        }
        protected PosixDateTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}