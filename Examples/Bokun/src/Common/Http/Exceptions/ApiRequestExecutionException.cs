using System;
using System.Runtime.Serialization;

namespace Common.Http.Exceptions
{
	public class ApiRequestExecutionException : ApplicationException
	{
		public ApiRequestExecutionException()
		{

		}
		public ApiRequestExecutionException(String message) : base(message)
		{

		}
		public ApiRequestExecutionException(String message, Exception ex) : base(message)
		{

		}
		protected ApiRequestExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}