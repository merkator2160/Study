using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Threading.Tasks;

namespace GrpcService.Client.Interceptors
{
	/// <summary>
	/// Article: https://anthonygiretti.com/2020/03/31/grpc-asp-net-core-3-1-global-error-handling-in-grpc-grpc-status-codes/
	/// </summary>
	public class AnthonyClientLoggerInterceptor : Interceptor
	{
		//private readonly ILogger<LoggerInterceptor> _logger;


		public AnthonyClientLoggerInterceptor()
		{

		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
		{
			LogCall(context.Method);

			var call = continuation(request, context);

			return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
		}
		private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task)
		{
			try
			{
				var response = await task;
				//_logger.LogDebug($"Response received: {response}");
				return response;
			}
			catch(RpcException ex)
			{
				//_logger.LogError($"Call error: {ex.Message}");
				return default;
			}
		}
		private void LogCall<TRequest, TResponse>(Method<TRequest, TResponse> method) where TRequest : class where TResponse : class
		{
			//_logger.LogDebug($"Starting call. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
		}
	}
}