using System;

namespace MicroServices.MassTransitTest
{
	internal class Program
	{
		private const String ConnectionString = "rabbitmq://localhost/MyQueue";


		static void Main(string[] args)
		{
			using(var worker = new TaskGenerator(ConnectionString))
			{
				worker.Run();

				Console.ReadKey();
			}
		}
	}
}