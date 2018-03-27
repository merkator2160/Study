using System;
using System.Runtime.Serialization;

namespace UsefulItems
{
    [Serializable]
    public class ExampleException : ApplicationException
    {
        public ExampleException()
        {

        }
        public ExampleException(String message) : base(message)
        {

        }
        public ExampleException(String message, Exception ex) : base(message)
        {

        }
        protected ExampleException(SerializationInfo info, StreamingContext contex) : base(info, contex)
        {

        }
    }
}