using MassTransit;
using MicroServices.MassTransitTest.Consumers;
using System;

namespace MicroServices.MassTransitTest
{
	public class TaskHandler : IDisposable
	{
		private readonly String _connectionString;
		private IServiceBus _bus;
		private Boolean _disposed;


		public TaskHandler(String connectionString)
		{
			_connectionString = connectionString;
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void Run()
		{
			_bus = ServiceBusFactory.New(x =>
			{
				x.UseRabbitMq();
				x.ReceiveFrom(_connectionString);
				x.Subscribe(subs =>
				{
					subs.Consumer<TaskConsumer>().Permanent();
				});
			});
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