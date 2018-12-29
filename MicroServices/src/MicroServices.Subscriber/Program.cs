using MicroServices.Contracts;
using MicroServices.Subscriber.Handlers;
using System;

namespace MicroServices.Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			var service = new MassTransitTaskHandler(CommonValues.ConnectionString, CommonValues.Login, CommonValues.Password);
			service.Run();

			Console.ReadKey();
		}
	}
}
