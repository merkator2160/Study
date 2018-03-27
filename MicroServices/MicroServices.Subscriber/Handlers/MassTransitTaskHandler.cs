using MassTransit;
using MicroServices.Contracts;
using MicroServices.Contracts.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroServices.Subscriber.Handlers
{
	public class MassTransitTaskHandler : IDisposable
	{
		private readonly IBusControl _bus;
		private Boolean _disposed;


		public MassTransitTaskHandler(String connectionString, String login, String password)
		{
			_bus = Bus.Factory.CreateUsingRabbitMq(configurator =>
			{
				var host = configurator.Host(new Uri(connectionString), h =>
				{
					h.Username(login);
					h.Password(password);
				});

				configurator.ReceiveEndpoint(host, CommonValues.QueueName, endpoint =>
				{
					endpoint.Handler<IMyTask>(context =>
					{
						Thread.Sleep(context.Message.ExecutionTime);     // artifical payload
						Console.WriteLine($"Received: {context.Message.TaskMessage}");

						return Task.FromResult(0);
					});
				});
			});
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void Run()
		{
			_bus.Start();
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			if(_disposed)
				return;

			_disposed = true;
			if(_bus != null)
			{
				_bus.Stop();
				(_bus as IDisposable)?.Dispose();
			}
		}
	}
}