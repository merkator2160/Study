namespace ApiClients.StackDriver.Interfaces
{
	public interface IStackDriverClient
	{
		IErrorReportingClient ErrorReporting { get; }
		IMetricClient Metric { get; }
	}
}