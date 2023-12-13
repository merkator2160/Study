using ApiClientsHttp.Finam;
using Autofac;
using Common.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Module = Autofac.Module;

namespace ApiClientsHttp.DependencyInjection
{
    public class HttpClientModule : Module
    {
        private readonly IConfiguration _configuration;


        public HttpClientModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConfiguration(_configuration, Assembly.GetExecutingAssembly());
            builder.RegisterType<FinamHttpClient>().AsSelf().AsImplementedInterfaces();
        }
    }
}