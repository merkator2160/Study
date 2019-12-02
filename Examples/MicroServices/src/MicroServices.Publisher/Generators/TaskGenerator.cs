using MicroServices.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MicroServices.Publisher.Generators
{
	public class TaskGenerator : IDisposable
	{
		private readonly ConnectionFactory _factory;
		private readonly IConnection _connection;
		private readonly IModel _testChannel;

		private Boolean _disposed;
		private Int32 _messageCounter;



		public TaskGenerator()
		{
			_factory = new ConnectionFactory();
			_connection = _factory.CreateConnection();
			_testChannel = _connection.CreateModel();

			_testChannel.BasicReturn += TestModelOnBasicReturn;
			_testChannel.BasicRecoverOk += TestModelOnBasicRecoverOk;

			_testChannel.ExchangeDeclare(CommonValues.ExchangeName, ExchangeType.Direct);
			_testChannel.QueueDeclare(CommonValues.QueueName, false, false, false, null);
			_testChannel.QueueBind(CommonValues.QueueName, CommonValues.ExchangeName, CommonValues.RoutingKey, null);

			ThreadPool.QueueUserWorkItem(SenderThread);
		}



		// THREADS ////////////////////////////////////////////////////////////////////////////////
		private void SenderThread(Object state)
		{
			while(!_disposed)
			{
				try
				{
					var messageBodyBytes = Encoding.UTF8.GetBytes($"Message {_messageCounter++}");
					_testChannel.BasicPublish(CommonValues.ExchangeName, CommonValues.RoutingKey, null, messageBodyBytes);

					Console.Clear();
					Console.WriteLine($"{CommonValues.QueueName} queue messages count: {_testChannel.MessageCount(CommonValues.QueueName)}");
					Thread.Sleep(1000);
				}
				catch(Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
		}


		// HANDLERS ///////////////////////////////////////////////////////////////////////////////
		private void TestModelOnBasicReturn(Object sender, BasicReturnEventArgs basicReturnEventArgs)
		{
			Console.WriteLine($"{nameof(TestModelOnBasicReturn)} arise!");
		}
		private void TestModelOnBasicRecoverOk(Object sender, EventArgs eventArgs)
		{
			Console.WriteLine($"{nameof(TestModelOnBasicRecoverOk)} arise!");
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			if(_disposed)
				return;

			_disposed = true;

			_testChannel?.Dispose();
			_connection?.Dispose();
		}
	}
}