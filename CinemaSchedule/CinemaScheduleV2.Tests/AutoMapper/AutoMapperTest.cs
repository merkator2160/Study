using AutoMapper;
using CinemaScheduleV2.Middleware.AutoMapper;
using System.Reflection;
using Xunit;

namespace CinemaScheduleV2.Tests.AutoMapper
{
    public class AutoMapperTest
    {
        [Fact]
        public void TheWholeAutomapperConfigurationTest()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperMiddleware).GetTypeInfo().Assembly);     // Dynamically load all configurations
            });

            mapperConfiguration.CompileMappings();
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}