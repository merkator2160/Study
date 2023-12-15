using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Server.Services
{
    public class RepeaterService : Repeater.RepeaterBase
    {
        private readonly ILogger<RepeaterService> _logger;
        private readonly ConcurrentQueue<String> _messageQueue;


        public RepeaterService(ILogger<RepeaterService> logger)
        {
            _logger = logger;
            _messageQueue = new ConcurrentQueue<String>();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public override Task<Response> SendEcho(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Response
            {
                Content = $"Echo: {request.Content}"
            });
        }
        public override async Task<Response> StartClientStream(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            await foreach (var item in requestStream.ReadAllAsync())
            {
                Trace.TraceInformation(item.Content);
            }
            
            return new Response()
            {
                Content = "Data received"
            };
        }
        public override async Task StartServerStream(Empty request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                await responseStream.WriteAsync(new Response
                {
                    Content = $"Message {i}"
                });
                await Task.Delay(1000);
            }
        }
        public override async Task StartEchoDuplexStreamStream(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    _messageQueue.Enqueue(message.Content);
                }
            });

            while (!readTask.IsCompleted)
            {
                if (!_messageQueue.IsEmpty)
                {
                    if (_messageQueue.TryDequeue(out var message))
                    {
                        await responseStream.WriteAsync(new Response
                        {
                            Content = $"Echo: {message}"
                        });
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            
            await readTask;
        }
    }
}
