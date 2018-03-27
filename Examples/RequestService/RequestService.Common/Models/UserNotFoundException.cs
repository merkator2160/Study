using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RequestService.Common.Models
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException()
        {
        }
        public UserNotFoundException(String message) : base(message)
        {
        }
        public UserNotFoundException(String message, Exception inner) : base(message, inner)
        {
        }
        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
