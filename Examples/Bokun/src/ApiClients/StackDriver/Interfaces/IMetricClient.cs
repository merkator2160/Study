using Google.Api;
using Google.Cloud.Monitoring.V3;
using System;
using System.Threading.Tasks;

namespace ApiClients.StackDriver.Interfaces
{
	public interface IMetricClient
	{
		Task<MetricDescriptor[]> GetMetricsAsync(Int32 numberOfMetrics);
		Task<MetricDescriptor> GetMetricAsync(String metricId);
		Task<MetricDescriptor> CreateMetricAsync(MetricDescriptor metricDescriptor);
		Task DeleteMetricAsync(String metricId);
		Task SendTimeSeriesAsync(TimeSeries[] timeSeries);
	}
}