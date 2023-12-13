using System.Runtime.Serialization;

namespace Common.Contracts.Exceptions.Application
{
    public class EnvironmentVariableNotFoundException : ApplicationException
    {
        public EnvironmentVariableNotFoundException()
        {

        }
        public EnvironmentVariableNotFoundException(String message) : base(message)
        {

        }
        public EnvironmentVariableNotFoundException(String message, Exception ex) : base(message)
        {

        }
        protected EnvironmentVariableNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}