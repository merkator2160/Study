using MicroServices.Contracts;
using MicroServices.Publisher.Generators;
using System;

namespace MicroServices.Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var generator = new MassTransitTaskGenerator(CommonValues.ConnectionString, CommonValues.Login, CommonValues.Password))
			{
				generator.Run();

				Console.ReadKey();
			}
		}
	}
}
