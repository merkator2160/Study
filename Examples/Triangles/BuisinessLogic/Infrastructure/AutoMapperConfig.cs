using AutoMapper;
using BuisinessLogic.Models;
using Contracts.Models;

namespace BuisinessLogic.Infrastructure
{
    public class AutoMapperConfig
    {
        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public static IMapper GetConfiguredMapper()
        {
            return new MapperConfiguration(RegisterMappings).CreateMapper();
        }
        private static void RegisterMappings(IMapperConfiguration cfg)
        {
            cfg.CreateMap<TriangleDto, Triangle>();
        }
    }
}
