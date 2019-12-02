using System;
using System.Runtime.Serialization;

namespace CodeForces.UsefulItems
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
		protected ExampleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}