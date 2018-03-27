using MassTransit;
using MicroServices.Contracts.Models;
using System;
using System.Threading;

namespace MicroServices.MassTransitTest
{
	public class TaskGenerator : IDisposable
	{
		private readonly IServiceBus _bus;
		private Boolean _disposed;


		public TaskGenerator(String connectionString)
		{
			_bus = ServiceBusFactory.New(x =>
			{
				x.UseRabbitMq();
				x.ReceiveFrom(connectionString);
			});
		}


		// THREADS ////////////////////////////////////////////////////////////////////////////////
		private void MessageGeneratorThread(Object state)
		{
			var i = 0;

			while(!_disposed)
			{
				try
				{
					var message = $"Message #{++i}";
					_bus.Publish<IMyTask>(new MyTask
					{
						TaskMessage = message,
						ExecutionTime = 0
					});
					Console.WriteLine(message);

					Thread.Sleep(500);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
					throw;
				}
			}
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void Run()
		{
			ThreadPool.QueueUserWorkItem(MessageGeneratorThread);
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			if(_disposed)
				return;

			_disposed = true;
			_bus?.Dispose();
		}
	}
}