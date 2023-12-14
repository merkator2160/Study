using ApiClientsHttp.DependencyInjection;
using ApiClientsHttp.Finam;
using ApiClientsHttp.Finam.Models.Config;
using Autofac;
using Common.DependencyInjection;
using Finam.TradeApi.Grpc.V1;
using Finam.TradeApi.Proto.V1;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Sandbox
{
    internal class Program
    {
        static void Main(String[] args)
        {
            using (var container = CreateContainer())
            {
                //UseHttp(container);
                UseGrpc(container);
                //UseGrpcStream(container);
            }
        }
        private static void UseHttp(IContainer container)
        {
            var client = container.Resolve<FinamHttpClient>();

            var checkTokenResult = client.CheckTokenAsync().Result;
            //var securities = client.GetSecuritiesAsync().Result;
            //var portfolio = client.GetPortfolioAsync().Result;
            //var orders = client.GetOrdersAsync().Result;
            //var spots = client.GetStopsAsync().Result;
        }
        private static void UseGrpc(IContainer container)
        {
            var config = container.Resolve<FinamHttpClientConfig>();
            using (var channel = GrpcChannel.ForAddress(config.BaseAddress))
            {
                var securitiesClient = new Securities.SecuritiesClient(channel);
                var securities = securitiesClient.GetSecurities(new GetSecuritiesRequest(), container.Resolve<Metadata>());
            }
        }
        private static async void UseGrpcStream(IContainer container)
        {
            var config = container.Resolve<FinamHttpClientConfig>();
            using (var channel = GrpcChannel.ForAddress(config.BaseAddress))
            {
                var eventsClient = new Events.EventsClient(channel);
                using (var call = eventsClient.GetEvents(container.Resolve<Metadata>()))
                {
                    Console.WriteLine("Starting background task to receive messages");

                    var readTask = Task.Run(async () =>
                    {
                        await foreach (var response in call.ResponseStream.ReadAllAsync())
                        {
                            Console.WriteLine(response);
                        }
                    });

                    Console.WriteLine("Starting to send messages");

                    await call.RequestStream.WriteAsync(new SubscriptionRequest()
                    {
                        OrderBookSubscribeRequest = new OrderBookSubscribeRequest()
                    });

                    Console.WriteLine("Disconnecting");

                    await call.RequestStream.CompleteAsync();
                    await readTask;
                }
            }
        }


        // SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            var configuration = CustomConfigurationProvider.CollectEnvironmentRelatedConfiguration();

            builder.RegisterInstance(configuration).AsSelf().AsImplementedInterfaces();
            builder.RegisterCommonConfiguration(configuration);
            builder.RegisterModule(new HttpClientModule(configuration));
            builder.RegisterInstance(new Metadata
            {
                {
                    "X-Api-Key", configuration.GetSection(nameof(FinamHttpClientConfig)).Get<FinamHttpClientConfig>().ApiKey
                }
            });

            return builder.Build();
        }
    }
}