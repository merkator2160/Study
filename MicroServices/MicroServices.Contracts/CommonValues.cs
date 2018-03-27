using System;

namespace MicroServices.Contracts
{
	public static class CommonValues
	{
		public const String Login = "guest";
		public const String Password = "guest";

		public const String ConnectionString = "rabbitmq://localhost/";

		public const String QueueName = "MyQueue";
		public const String RoutingKey = "TestRoutingKey";
		public const String ExchangeName = "TestExchange";
	}
}