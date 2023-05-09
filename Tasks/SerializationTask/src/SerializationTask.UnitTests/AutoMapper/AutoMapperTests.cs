using AutoMapper;
using SerializationTask.Main.Core.AutoMapper;
using System.Reflection;
using Xunit;

namespace SerializationTask.Tests.AutoMapper
{
	public class AutoMapperTests
	{
		[Fact]
		public void ConfigurationTest()
		{
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.AddMaps(typeof(AutoMapperCreator).GetTypeInfo().Assembly);
			});

			mapperConfiguration.CompileMappings();
			mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}