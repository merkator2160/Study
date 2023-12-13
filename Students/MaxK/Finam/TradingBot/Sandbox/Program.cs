using ApiClientsHttp.DependencyInjection;
using ApiClientsHttp.Finam;
using Autofac;
using Common.DependencyInjection;
using Finam.TradeApi.Grpc.V1;
using Grpc.Core;
using Grpc.Net.Client;

namespace Sandbox
{
    internal class Program
    {
        static void Main(String[] args)
        {
            //UseHttp();
            UseGrpc();
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
                    "X-Api-Key", "CAEQpJ4IGhhndv/gVDxraomGF072/CxeAg8iA6mUrks="
                }
            });

            return builder.Build();
        }
        private static void UseHttp()
        {
            using (var container = CreateContainer())
            {
                var client = container.Resolve<FinamHttpClient>();

                var checkTokenResult = client.CheckTokenAsync().Result;
                //var securities = client.GetSecuritiesAsync().Result;
                //var portfolio = client.GetPortfolioAsync().Result;
                //var orders = client.GetOrdersAsync().Result;
                //var spots = client.GetStopsAsync().Result;
            }
        }
        private static async void UseGrpc()
        {
            using (var container = CreateContainer())
            {
                using (var channel = GrpcChannel.ForAddress("https://trade-api.finam.ru"))
                {
                    var securitiesClient = new Securities.SecuritiesClient(channel);
                    var securities = securitiesClient.GetSecurities(new GetSecuritiesRequest(), container.Resolve<Metadata>());

                    //var eventsClient = new Events.EventsClient(channel);
                    //using (var call = eventsClient.GetEvents(container.Resolve<Metadata>()))
                    //{
                    //    Console.WriteLine("Starting background task to receive messages");

                    //    var readTask = Task.Run(async () =>
                    //    {
                    //        await foreach (var response in call.ResponseStream.ReadAllAsync())
                    //        {
                    //            Console.WriteLine(response);
                    //        }
                    //    });

                    //    Console.WriteLine("Starting to send messages");

                    //    await call.RequestStream.WriteAsync(new SubscriptionRequest()
                    //    {
                    //        OrderBookSubscribeRequest = new OrderBookSubscribeRequest()
                    //    });

                    //    Console.WriteLine("Disconnecting");

                    //    await call.RequestStream.CompleteAsync();
                    //    await readTask;
                    //}
                }
            }
        }
    }
}