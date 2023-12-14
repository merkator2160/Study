using Grpc.Core;
using System.Collections.Concurrent;

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
        public override Task<Reply> SendEcho(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Reply
            {
                Message = $"Echo: {request.Message}"
            });
        }
        public override async Task StartEventsStream(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Reply> responseStream, ServerCallContext context)
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    _messageQueue.Enqueue(message.Message);
                }
            });

            while (!readTask.IsCompleted)
            {
                if (!_messageQueue.IsEmpty)
                {
                    if (_messageQueue.TryDequeue(out var message))
                    {
                        await responseStream.WriteAsync(new Reply
                        {
                            Message = $"Echo: {message}"
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
