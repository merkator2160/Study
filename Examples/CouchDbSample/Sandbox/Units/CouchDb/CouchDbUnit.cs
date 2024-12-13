using Autofac;
using Common.DependencyInjection;
using CouchDb.Client;
using CouchDb.Client.DependencyInjection;
using CouchDB.Driver;
using CouchDB.Driver.Types;
using Sandbox.Units.CouchDb.Models;

namespace Sandbox.Units.CouchDb
{
    internal class CouchDbUnit
    {
        private const String _dbName = "test";


        public static void Run()
        {
            using (var container = CreateContainer())
            {
                //var config = container.Resolve<CouchDbConfig>();
                //var client = new CouchClient(config.Url, builder =>
                //{
                //    builder.UseBasicAuthentication(config.Login, config.Password);
                //});
                //var db = client.GetOrCreateDatabaseAsync<TestOneDb>(_dbName, discriminator: "one");

                //GetDatabaseNames(container);
                //CreateUser(container);

                //AddDataOne(client);
                //AddDataTwo(client);

                //GetDataOne(client);
                //GetDataTwo(client);
            }
        }

        private static void GetDatabaseNames(IContainer container)
        {
            var context = container.Resolve<DataContext>();
            var databaseNames = context.Client.GetDatabasesNamesAsync().Result;
            foreach (var x in databaseNames)
            {
                Console.WriteLine(x);
            }
        }
        private static void CreateUser(IContainer container)
        {
            var context = container.Resolve<DataContext>();
            var userDb = context.Client.GetUsersDatabase();
            var luke = userDb.AddAsync(new CouchUser(name: "luke", password: "lasersword")).Result;

            foreach (var x in userDb)
            {
                Console.WriteLine(x);
            }
        }

        private static void AddDataOne(CouchClient client)
        {
            var testDb = client.GetDatabase<TestOneDb>(_dbName, discriminator: "one");
            for (int i = 0; i < 10; i++)
            {
                testDb.AddAsync(new TestOneDb()
                {
                    ChatId = Random.Shared.Next(),
                    UserId = Random.Shared.Next()
                }).Wait();
            }
        }
        private static void AddDataTwo(CouchClient client)
        {
            var testDb = client.GetDatabase<TestTwoDb>(_dbName);
            for (int i = 0; i < 10; i++)
            {
                testDb.AddAsync(new TestTwoDb()
                {
                    Data = $"Data: {Random.Shared.Next()}"
                }).Wait();
            }
        }

        private static void GetDataOne(CouchClient client)
        {
            var testDb = client.GetDatabase<TestOneDb>(_dbName, discriminator: "one");
            //var testDb = client.GetDatabase<TestOneDb>(_dbName);
            foreach (var x in testDb)
            {
                Console.WriteLine(x.ChatId);
            }
        }
        private static void GetDataTwo(CouchClient client)
        {
            var testDb = client.GetDatabase<TestTwoDb>(_dbName, discriminator: "two");
            //var testDb = client.GetDatabase<TestTwoDb>(_dbName);
            foreach (var x in testDb)
            {
                if (String.IsNullOrWhiteSpace(x.Data))
                {
                    Console.WriteLine("null");
                    continue;
                }

                Console.WriteLine(x.Data);
            }
        }



        // SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            var configuration = CustomConfigurationProvider.CollectEnvironmentRelatedConfiguration();
            var assemblies = Collector.LoadAssemblies("Nesusvet.TelegramBot");

            builder.RegisterCommonConfiguration(configuration);
            builder.RegisterConfiguration(configuration, assemblies);

            builder.RegisterInstance(configuration).AsSelf().AsImplementedInterfaces();
            builder.RegisterModule(new DatabaseModule(configuration));

            return builder.Build();
        }
    }
}