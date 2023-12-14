using Autofac;
using Common.Contracts.Config;
using Common.DependencyInjection;
using Common.DependencyInjection.Modules;
using Grpc.Core;
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
                //MakeGreeterCall(container);
                UseGreeterStream(container);
            }
        }


        // SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
        private static void MakeGreeterCall(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var greeterClient = new Repeater.RepeaterClient(channel);
                var reply = greeterClient.SendEcho(new Request()
                {
                    Message = "Test"
                });
            }
        }
        private static async void UseGreeterStream(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var repeaterClient = new Repeater.RepeaterClient(channel);
                using (var call = repeaterClient.StartEventsStream())
                {
                    var readTask = Task.Run(async () =>
                    {
                        await foreach (var response in call.ResponseStream.ReadAllAsync())
                        {
                            Console.WriteLine(response);
                        }
                    });

                    
                    await call.RequestStream.WriteAsync(new Request()
                    {
                        Message = "Test"
                    });

                    await call.RequestStream.CompleteAsync();
                    await readTask;
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
            builder.RegisterConfiguration(configuration, assemblies);
            
            builder.RegisterModule(new AutoMapperModule(assemblies));

            return builder.Build();
        }
    }
}