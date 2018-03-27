using AutoMapper;
using CinemaSchedule.BusinessLogic.Models;
using CinemaSchedule.DataBase.Models;
using CinemaSchedule.Models.ApiModels;

namespace CinemaSchedule.AutoMapper
{
    internal static class AutoMapperConfig
    {
        public static IMapper GetConfiguredMapper()
        {
            return new MapperConfiguration(RegisterMappings).CreateMapper();
        }
        public static void RegisterMappings(IMapperConfigurationExpression cfg)
        {
            MapToDtoEntityes(cfg);
            MapToDbEntityes(cfg);
            MapToAmEntityes(cfg);
        }
        private static void MapToDtoEntityes(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CinemaDb, CinemaDto>();
            cfg.CreateMap<FilmDb, FilmDto>();
            cfg.CreateMap<SessionDb, SessionDto>();
            cfg.CreateMap<PostSessionAm, SessionDto>();
            cfg.CreateMap<PutSessionAm, SessionDto>();
        }
        public static void MapToDbEntityes(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CinemaDto, CinemaDb>();
            cfg.CreateMap<FilmDto, FilmDb>();
            cfg.CreateMap<SessionDto, SessionDb>();
        }
        public static void MapToAmEntityes(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CinemaDto, CinemaAm>();
            cfg.CreateMap<SessionDto, SessionAm>();
            cfg.CreateMap<FilmDto, FilmAm>();
        }
    }
}