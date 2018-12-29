using System;

namespace MicroServices.Contracts.Models
{
	public interface IMyTask
	{
		String TaskMessage { get; set; }
		Int32 ExecutionTime { get; set; }
	}
}