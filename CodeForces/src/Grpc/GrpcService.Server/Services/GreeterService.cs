using Grpc.Core;
using GrpcService.Server.Services.Models.Exceptions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcService.Server.Services
{
	public class GreeterService : Greeter.GreeterBase
	{
		private readonly ILogger<GreeterService> _logger;


		public GreeterService(ILogger<GreeterService> logger)
		{
			_logger = logger;
		}


		// ACTIONS ////////////////////////////////////////////////////////////////////////////////
		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			//throw new Exception("Exception message");
			throw new CustomApplicationException("CustomApplicationException message");

			return Task.FromResult(new HelloReply
			{
				Message = $"Hello {request.Name}"
			});
		}
		public override async Task SayHellos(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
		{
			var i = 0;
			while(!context.CancellationToken.IsCancellationRequested)
			{
				var message = $"How are you {request.Name}? {++i}";
				_logger.LogInformation($"Sending greeting {message}.");

				await responseStream.WriteAsync(new HelloReply { Message = message });

				await Task.Delay(1000); // Gotta look busy
			}
		}
	}
}
