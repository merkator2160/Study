using Autofac;
using Common.Contracts.Config;
using Common.DependencyInjection;
using Common.DependencyInjection.Modules;
using Google.Protobuf.WellKnownTypes;
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
                CallBasicGreeterCall(container);
                //CallGreeterClientStream(container);
                //CallGreeterServerStream(container);
                //CallGreeterDuplexStream(container);
            }
        }


        // SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
        private static void CallBasicGreeterCall(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var greeterClient = new Repeater.RepeaterClient(channel);
                var reply = greeterClient.SendEcho(new Request()
                {
                    Content = "Test"
                });
            }
        }
        private static async void CallGreeterClientStream(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var repeaterClient = new Repeater.RepeaterClient(channel);
                using (var clientStream = repeaterClient.StartClientStream())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await clientStream.RequestStream.WriteAsync(new Request
                        {
                            Content = $"Message {i}"
                        });
                        await Task.Delay(1000);
                    }

                    await clientStream.RequestStream.CompleteAsync();
                    var response = await clientStream.ResponseAsync;

                    Console.WriteLine(response.Content);
                }
            }
        }
        private static async void CallGreeterServerStream(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var repeaterClient = new Repeater.RepeaterClient(channel);
                using (var clientStream = repeaterClient.StartClientStream())
                {
                    var serverData = repeaterClient.StartServerStream(new Empty());
                    
                    var responseStream = serverData.ResponseStream;
                    while (await responseStream.MoveNext(new CancellationToken()))
                    {
                        var response = responseStream.Current;
                        Console.WriteLine(response.Content);
                    }
                }
            }
        }
        private static async void MakeGreeterDuplexStream(IContainer container)
        {
            var serverConfig = container.Resolve<ServerConfig>();
            using (var channel = GrpcChannel.ForAddress(serverConfig.BaseAddress))
            {
                var repeaterClient = new Repeater.RepeaterClient(channel);
                using (var call = repeaterClient.StartEchoDuplexStreamStream())
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
                        Content = "Test"
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