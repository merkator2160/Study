using System;

namespace MicroServices.Contracts.Models
{
	public class MyTask : IMyTask
	{
		public string TaskMessage { get; set; }
		public Int32 ExecutionTime { get; set; }
	}
}