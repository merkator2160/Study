using MassTransit;
using MicroServices.Contracts.Models;
using System;
using System.Threading;

namespace MicroServices.Publisher.Generators
{
	public class MassTransitTaskGenerator : IDisposable
	{
		private readonly IBusControl _bus;
		private Boolean _disposed;


		public MassTransitTaskGenerator(String connectionString, String login, String password)
		{
			_bus = Bus.Factory.CreateUsingRabbitMq(configurator =>
			{
				configurator.Host(new Uri(connectionString), h =>
				{
					h.Username(login);
					h.Password(password);
				});
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
			_bus.Start();
			ThreadPool.QueueUserWorkItem(MessageGeneratorThread);
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