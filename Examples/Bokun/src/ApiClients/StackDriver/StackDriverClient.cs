using ApiClients.StackDriver.Interfaces;

namespace ApiClients.StackDriver
{
	public class StackDriverClient : IStackDriverClient
	{
		public StackDriverClient(IErrorReportingClient errorReportingClient, IMetricClient metricClient)
		{
			ErrorReporting = errorReportingClient;
			Metric = metricClient;
		}


		// IStackDriverClient /////////////////////////////////////////////////////////////////////
		public IErrorReportingClient ErrorReporting { get; }
		public IMetricClient Metric { get; }
	}
}