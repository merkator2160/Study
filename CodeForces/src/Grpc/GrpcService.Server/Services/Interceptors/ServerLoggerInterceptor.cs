using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Server.Services.Interceptors
{
	public class ServerLoggerInterceptor : Interceptor
	{
		private readonly ILogger<ServerLoggerInterceptor> _logger;


		public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
		{
			_logger = logger;
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
		{
			LogCall<TRequest, TResponse>(MethodType.DuplexStreaming, context);
			return continuation(request, context);
		}
		public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
		{
			LogCall<TRequest, TResponse>(MethodType.DuplexStreaming, context);
			return base.ClientStreamingServerHandler(requestStream, context, continuation);
		}
		public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
		{
			LogCall<TRequest, TResponse>(MethodType.DuplexStreaming, context);
			return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
		}
		public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
		{
			LogCall<TRequest, TResponse>(MethodType.DuplexStreaming, context);
			return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
		}
		private void LogCall<TRequest, TResponse>(MethodType methodType, ServerCallContext context) where TRequest : class where TResponse : class
		{
			_logger.LogWarning($"Starting call. Type: {methodType}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
			WriteMetadata(context.RequestHeaders, "caller-user");
			WriteMetadata(context.RequestHeaders, "caller-machine");
			WriteMetadata(context.RequestHeaders, "caller-os");

			void WriteMetadata(Metadata headers, String key)
			{
				var headerValue = headers.SingleOrDefault(h => h.Key == key)?.Value;
				_logger.LogWarning($"{key}: {headerValue ?? "(unknown)"}");
			}
		}
	}
}