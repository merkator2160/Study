using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcService.Client.Interceptors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcService.Client
{
	class Program
	{
		static async Task Main(String[] args)
		{
			using(var channel = GrpcChannel.ForAddress("https://localhost:5001"))
			{
				var invoker = channel.Intercept(new ClientLoggerInterceptor());
				var client = new Greeter.GreeterClient(invoker);

				await ExecuteSingleCall(client);
				//await ExecuteStreamingCall(client);

				Console.WriteLine("Shutting down");
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		private static async Task ExecuteSingleCall(Greeter.GreeterClient client)
		{
			try
			{
				var reply = await client.SayHelloAsync(new HelloRequest
				{
					Name = "GreeterClient"
				});
				Console.WriteLine($"Greeting: {reply.Message}");
			}
			catch(RpcException ex)
			{
				// ouch!
				// lets print the gRPC error message
				// which is "Length of `Name` cannot be more than 10 characters"
				Console.WriteLine(ex.Status.Detail);
				// lets access the error code, which is `INVALID_ARGUMENT`
				Console.WriteLine(ex.Status.StatusCode);
				// Want its int version for some reason?
				// you shouldn't actually do this, but if you need for debugging,
				// you can access `e.Status.StatusCode` which will give you `3`
				Console.WriteLine((Int32)ex.Status.StatusCode);
				// Want to take specific action based on specific error?	https://grpc.io/docs/guides/error/
				if(ex.Status.StatusCode == StatusCode.InvalidArgument)
				{
					// do your thing
				}
			}
		}
		private static async Task ExecuteStreamingCall(Greeter.GreeterClient client)
		{
			var cts = new CancellationTokenSource();
			cts.CancelAfter(TimeSpan.FromSeconds(10.5));

			using(var call = client.SayHellos(new HelloRequest
			{
				Name = "GreeterClient"
			}, cancellationToken: cts.Token))
			{
				try
				{
					await foreach(var message in call.ResponseStream.ReadAllAsync())
					{
						Console.WriteLine($"Greeting: {message.Message}");
					}
				}
				catch(RpcException ex) when(ex.StatusCode == StatusCode.Cancelled)
				{
					Console.WriteLine("Stream cancelled.");
				}
			}
		}
	}
}