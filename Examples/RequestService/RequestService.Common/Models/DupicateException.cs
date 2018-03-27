using System;
using System.Runtime.Serialization;

namespace RequestService.Common.Models
{
    public class DupicateException :ApplicationException
    {
        public DupicateException()
        {
        }
        public DupicateException(String message) : base(message)
        {
        }
        public DupicateException(String message, Exception inner) : base(message, inner)
        {
        }
        protected DupicateException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}
