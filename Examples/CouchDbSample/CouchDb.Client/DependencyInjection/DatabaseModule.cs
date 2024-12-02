using Autofac;
using Common.DependencyInjection;
using CouchDb.Client.InMemory;
using CouchDb.Client.Models.Config;
using CouchDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Module = Autofac.Module;

namespace CouchDb.Client.DependencyInjection
{
    public class DatabaseModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly Assembly _currentAssembly;


        public DatabaseModule(IConfiguration configuration)
        {
            _currentAssembly = Assembly.GetExecutingAssembly();
            _configuration = configuration;
        }


        // COMPONENT REGISTRATION /////////////////////////////////////////////////////////////////
        protected override void Load(ContainerBuilder builder)
        {
            RegisterContext(builder);
            RegisterRepositories(builder);
            builder.RegisterLocalConfiguration(_configuration);
        }
        public void RegisterContext(ContainerBuilder builder)
        {
            builder.RegisterInstance(CreateClient(_configuration));
            builder.RegisterType<DataContext>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        public void RegisterRepositories(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(_currentAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsSelf()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(_currentAssembly)
                .Where(t => t.Name.EndsWith("Wrapper"))
                .AsImplementedInterfaces();

            builder.RegisterType<CaptchaInMemoryUserStorage>().SingleInstance();
            builder.RegisterType<RepositoryBundle>();
        }


        // CONTEXT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private static CouchClient CreateClient(IConfiguration configuration)
        {
            var config = configuration.GetSection("CouchDbConfig").Get<CouchDbConfig>();
            var client = new CouchClient(config.Url, builder =>
            {
                builder.UseBasicAuthentication(config.Login, config.Password);
            });

            return client;
        }
    }
}