using System;
using System.Threading.Tasks;

namespace ApiClients.StackDriver.Interfaces
{
	public interface IErrorReportingClient
	{
		Task SendReportAsync(Exception ex);
		void SendReport(Exception ex);
	}
}