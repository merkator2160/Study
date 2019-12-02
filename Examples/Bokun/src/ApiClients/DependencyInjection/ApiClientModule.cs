using ApiClients.StackDriver;
using Autofac;
using Common.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Module = Autofac.Module;

namespace ApiClients.DependencyInjection
{
	public class ApiClientModule : Module
	{
		private readonly IConfiguration _configuration;
		private readonly Assembly _currentAssembly;
		private readonly Assembly _callingAssembly;


		public ApiClientModule(IConfiguration configuration)
		{
			_callingAssembly = Assembly.GetCallingAssembly();
			_currentAssembly = Assembly.GetAssembly(typeof(ApiClientModule));
			_configuration = configuration;
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		protected override void Load(ContainerBuilder builder)
		{
			RegisterClients(builder);
			builder.RegisterConfiguration(_configuration, _currentAssembly);
		}
		public void RegisterClients(ContainerBuilder builder)
		{
			var callingAssemblyName = _callingAssembly.GetName();
			builder.RegisterType<ErrorReportingClient>()
				.WithParameters(new[]
				{
					new NamedParameter("serviceName", callingAssemblyName.Name),
					new NamedParameter("serviceVersion", callingAssemblyName.Version.ToString())
				})
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder
				.RegisterAssemblyTypes(_currentAssembly)
				.Where(p => p.Name.EndsWith("Client"))
				.Except<ErrorReportingClient>()
				.AsSelf()
				.AsImplementedInterfaces();
		}
	}
}