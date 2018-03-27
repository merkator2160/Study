using CinemaSchedule.AutoMapper;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Services;
using CinemaSchedule.DataBase;
using CinemaSchedule.DataBase.Interfaces;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace CinemaSchedule.Unity.ContainerExtensions
{
    public static class UnityWebApiConfig
    {
        public static void RegisterComponents()
        {
            var isDebugging = ((CompilationSection)ConfigurationManager.GetSection("system.web/compilation")).Debug;

            var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            container.RegisterInstance(AutoMapperConfig.GetConfiguredMapper());
            container.RegisterType<IUniteOfWork, UnitOfWork>(new InjectionConstructor(new InjectionParameter(new DataContext("CinemaScheduleDb"))));
            container.RegisterType<ICinemaService, CinemaService>();
            container.RegisterType<IFilmService, FilmService>();
            container.RegisterType<ISessionService, SessionService>();
        }
    }
}