using MicroServices.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace MicroServices.Subscriber.Handlers
{
	public class TaskHandler : IDisposable
	{
		private Boolean _disposed;
		private ConnectionFactory _factory;
		private readonly IConnection _connection;
		private readonly IModel _testChannel;
		private readonly EventingBasicConsumer _consumer;
		private readonly String _consumerTag;


		public TaskHandler()
		{
			_factory = new ConnectionFactory();
			_connection = _factory.CreateConnection();
			_testChannel = _connection.CreateModel();

			_testChannel.QueueDeclare(CommonValues.QueueName, false, false, false, null);
			_testChannel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

			_consumer = new EventingBasicConsumer(_testChannel);
			_consumer.Received += ConsumerOnReceived;
			_testChannel.BasicReturn += TestModelOnBasicReturn;
			_testChannel.BasicRecoverOk += TestModelOnBasicRecoverOk;
			_consumerTag = _testChannel.BasicConsume(CommonValues.QueueName, false, _consumer);
		}


		// HANDLERS ///////////////////////////////////////////////////////////////////////////////
		private void ConsumerOnReceived(Object sender, BasicDeliverEventArgs basicDeliverEventArgs)
		{
			var body = basicDeliverEventArgs.Body;
			var str = Encoding.UTF8.GetString(body);

			Thread.Sleep(2000); // artifical payload
			Console.WriteLine(str);

			_testChannel.BasicAck(basicDeliverEventArgs.DeliveryTag, false);
		}
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

			_testChannel?.BasicCancel(_consumerTag);
			_testChannel?.Dispose();
			_connection?.Dispose();
		}
	}
}