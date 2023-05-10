using AutoMapper;
using SerializationTask.Common.DependencyInjection;

namespace SerializationTask.UnitTests
{
    public class AutoMapperTests
	{
		[Fact]
		public void ConfigurationTest()
		{
            var serializationTaskAssemblies = Collector.LoadAssemblies("SerializationTask");
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(serializationTaskAssemblies);
            });

            mapperConfiguration.CompileMappings();
            mapperConfiguration.AssertConfigurationIsValid();
        }
	}
}