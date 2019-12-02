using ApiClients.StackDriver.Interfaces;
using Google.Api;
using Google.Cloud.Monitoring.V3;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClients.StackDriver
{
	public class MetricClient : IMetricClient
	{
		private const String _googleProjectEnvironmentVariableName = "GOOGLE_PROJECT_ID";
		private const String _googleApplicationCredentialsEnvironmentVariableName = "GOOGLE_APPLICATION_CREDENTIALS";
		private readonly ProjectName _projectName;
		private readonly String _googleProjectId;

		private readonly MetricServiceClient _client;


		public MetricClient()
		{
			_googleProjectId = Environment.GetEnvironmentVariable(_googleProjectEnvironmentVariableName);
			if(String.IsNullOrWhiteSpace(_googleProjectId))
				throw new ArgumentNullException($"Environment variable was not found: \"{_googleProjectEnvironmentVariableName}\"!");

			var googleApplicationCredentialsPath = Environment.GetEnvironmentVariable(_googleApplicationCredentialsEnvironmentVariableName);
			if(String.IsNullOrWhiteSpace(googleApplicationCredentialsPath))
				throw new ArgumentNullException($"Environment variable was not found: \"{_googleApplicationCredentialsEnvironmentVariableName}\"!");

			_projectName = new ProjectName(_googleProjectId);
			_client = MetricServiceClient.Create();
		}


		// IMetricClient //////////////////////////////////////////////////////////////////////////
		public async Task<MetricDescriptor[]> GetMetricsAsync(Int32 numberOfMetrics = 100)
		{
			return (await _client.ListMetricDescriptorsAsync(_projectName).ReadPageAsync(numberOfMetrics)).ToArray();
		}
		public async Task<MetricDescriptor> GetMetricAsync(String metricId)
		{
			return await _client.GetMetricDescriptorAsync($"projects/{_googleProjectId}/metricDescriptors/{metricId}");
		}
		public async Task<MetricDescriptor> CreateMetricAsync(MetricDescriptor metricDescriptor)
		{
			return await _client.CreateMetricDescriptorAsync(new CreateMetricDescriptorRequest
			{
				ProjectName = _projectName,
				MetricDescriptor = metricDescriptor
			});
		}
		public async Task DeleteMetricAsync(String metricId)
		{
			await _client.DeleteMetricDescriptorAsync($"projects/{_googleProjectId}/metricDescriptors/{metricId}");
		}
		public async Task SendTimeSeriesAsync(TimeSeries[] timeSeries)
		{
			await _client.CreateTimeSeriesAsync(_projectName, timeSeries);
		}
	}
}