using ApiClients.StackDriver.Interfaces;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.ErrorReporting.V1Beta1;
using System;
using System.Threading.Tasks;

namespace ApiClients.StackDriver
{
	public class ErrorReportingClient : IErrorReportingClient
	{
		private const String _googleProjectEnvironmentVariableName = "GOOGLE_PROJECT_ID";
		private const String _googleApplicationCredentialsEnvironmentVariableName = "GOOGLE_APPLICATION_CREDENTIALS";
		private readonly ProjectName _projectName;
		private readonly String _serviceName;
		private readonly String _serviceVersion;
		private readonly ReportErrorsServiceClient _client;


		public ErrorReportingClient(String serviceName, String serviceVersion)
		{
			var googleProjectId = Environment.GetEnvironmentVariable(_googleProjectEnvironmentVariableName);
			if(String.IsNullOrWhiteSpace(googleProjectId))
				throw new ArgumentNullException($"Environment variable was not found: \"{_googleProjectEnvironmentVariableName}\"!");

			var googleApplicationCredentialsPath = Environment.GetEnvironmentVariable(_googleApplicationCredentialsEnvironmentVariableName);
			if(String.IsNullOrWhiteSpace(googleApplicationCredentialsPath))
				throw new ArgumentNullException($"Environment variable was not found: \"{_googleApplicationCredentialsEnvironmentVariableName}\"!");

			_projectName = new ProjectName(googleProjectId);
			_serviceName = serviceName;
			_serviceVersion = serviceVersion;
			_client = ReportErrorsServiceClient.Create();
		}


		// IErrorReportingClient /////////////////////////////////////////////////////////////////////
		public async Task SendReportAsync(Exception ex)
		{
			await _client.ReportErrorEventAsync(_projectName, new ReportedErrorEvent()
			{
				Message = ex.ToString(),
				ServiceContext = new ServiceContext()
				{
					Service = _serviceName,
					Version = _serviceVersion
				}
			});
		}
		public void SendReport(Exception ex)
		{
			_client.ReportErrorEvent(_projectName, new ReportedErrorEvent()
			{
				Message = ex.ToString(),
				ServiceContext = new ServiceContext()
				{
					Service = _serviceName,
					Version = _serviceVersion
				}
			});
		}
	}
}