using MassTransit;
using MicroServices.Contracts.Models;
using System;
using System.Threading;

namespace MicroServices.MassTransitTest.Consumers
{
	internal class TaskConsumer : Consumes<IMyTask>.Context
	{
		public void Consume(IConsumeContext<IMyTask> context)
		{
			Thread.Sleep(context.Message.ExecutionTime);     // artifical payload
			Console.WriteLine($"Received: {context.Message.TaskMessage}");
		}
	}
}