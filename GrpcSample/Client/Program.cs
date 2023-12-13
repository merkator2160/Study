using Autofac;
using Common.DependencyInjection;
using Common.DependencyInjection.Modules;
using Grpc.Net.Client;
using Server;
using System.Reflection;

namespace Client
{
    internal class Program
    {
        static void Main(String[] args)
        {
            using (var container = CreateContainer())
            {
                using (var channel = GrpcChannel.ForAddress("https://localhost:7120"))
                {
                    var greeterClient = new Greeter.GreeterClient(channel);
                    var reply = greeterClient.SayHello(new HelloRequest()
                    {
                        Name = "Test"
                    });
                }
            }
        }


        // SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
        private static IContainer CreateContainer()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var commonAssembly = Collector.GetAssembly("Common");
            var assemblies = new[] { currentAssembly, commonAssembly };

            var builder = new ContainerBuilder();
            var configuration = CustomConfigurationProvider.CollectEnvironmentRelatedConfiguration();

            builder.RegisterInstance(configuration).AsSelf().AsImplementedInterfaces();
            builder.RegisterServices(currentAssembly);
            builder.RegisterConfiguration(configuration, currentAssembly);
            
            builder.RegisterModule(new AutoMapperModule(assemblies));

            return builder.Build();
        }
    }
}