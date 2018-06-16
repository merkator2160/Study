using AutoMapper;
using SerializationTask.Main.Config.AutoMapper;
using System.Reflection;
using Xunit;

namespace SerializationTask.Tests.AutoMapper
{
	public class AutoMapperTest
	{
		[Fact]
		public void TheWholeAutomapperConfigurationTest()
		{
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfiles(typeof(AutoMapperCreator).GetTypeInfo().Assembly);     // Dynamically load all configurations
			});

			mapperConfiguration.CompileMappings();
			mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}