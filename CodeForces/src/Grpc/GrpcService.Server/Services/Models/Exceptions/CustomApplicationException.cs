using System;
using System.Runtime.Serialization;

namespace GrpcService.Server.Services.Models.Exceptions
{
	public class CustomApplicationException : ApplicationException
	{
		public CustomApplicationException()
		{

		}
		public CustomApplicationException(String message) : base(message)
		{

		}
		public CustomApplicationException(String message, Exception ex) : base(message)
		{

		}
		protected CustomApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}