using AutoMapper;
using Common.DependencyInjection;
using Xunit;

namespace UnitTests
{
	public class AutoMapperTest
	{
		[Fact]
		public void AutoMapperConfigurationTest()
		{
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddMaps(Collector.LoadAssemblies("Sandbox"));
			});

			mapperConfiguration.CompileMappings();
			mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}