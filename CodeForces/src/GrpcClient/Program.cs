using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
	class Program
	{
		static void Main(String[] args)
		{
			MainAsync(args).GetAwaiter().GetResult();
		}
		static async Task MainAsync(String[] args)
		{
			using(var channel = GrpcChannel.ForAddress("https://localhost:5001"))
			{
				var client = new Greeter.GreeterClient(channel);
				var reply = await client.SayHelloAsync(new HelloRequest
				{
					Name = "GreeterClient"
				});

				Console.WriteLine("Greeting: " + reply.Message);
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
