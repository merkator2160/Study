using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcService.Server.Services.Interceptors
{
	/// <summary>
	/// Article: https://anthonygiretti.com/2020/03/31/grpc-asp-net-core-3-1-global-error-handling-in-grpc-grpc-status-codes/
	/// </summary>
	public class AnthonyLoggerInterceptor : Interceptor
	{
		private readonly ILogger<AnthonyLoggerInterceptor> _logger;


		public AnthonyLoggerInterceptor(ILogger<AnthonyLoggerInterceptor> logger)
		{
			_logger = logger;
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
		{
			LogCall(context);
			try
			{
				return await continuation(request, context);
			}
			catch(ApplicationException ex)
			{
				_logger.LogError(ex, $"A business logic exception occured when calling {context.Method}");

				throw new RpcException(new Status(StatusCode.Internal, "Business logic exception!"), CreateMetadata(ex));
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, $"An error occured when calling {context.Method}");
#if DEBUG
				throw new RpcException(new Status(StatusCode.Internal, "Internal server error!"), CreateMetadata(ex));
#else
				throw new RpcException(new Status(StatusCode.Internal, "Internal server error!"));
#endif
			}

		}
		private void LogCall(ServerCallContext context)
		{
			var httpContext = context.GetHttpContext();
			_logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
		}
		private Metadata CreateMetadata(Exception ex)
		{
			return new Metadata
			{
				new Metadata.Entry("Name", "the value was empty"),
				new Metadata.Entry("Message", ex.Message),
				//new Metadata.Entry("StackTrace", ex.StackTrace)	// TODO: Id doesn't work, why? - Large error detail payloads may run into protocol limits (like max headers size), effectively losing the original error.
			};
		}
	}
}