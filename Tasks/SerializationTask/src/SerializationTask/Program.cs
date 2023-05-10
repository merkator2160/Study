using Autofac;
using SerializationTask.Common.Contracts.Const;
using SerializationTask.Common.DependencyInjection;
using SerializationTask.Common.DependencyInjection.Modules;
using SerializationTask.Database.DependencyInjection;
using SerializationTask.Services.DataProviders;
using SerializationTask.Services.Interfaces;
using SerializationTask.Services.Models.Config;

namespace SerializationTask
{
    internal class Program
    {
        static void Main(String[] args)
        {
            using (var container = ConfigureContainer())
            {
                container.Resolve<ApplicationContext>().Run();
            }
        }


        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private static IContainer ConfigureContainer()
        {
            var assemblies = Collector.LoadAssemblies("SerializationTask");
            var builder = new ContainerBuilder();
            var configuration = CustomConfigurationProvider.CollectEnvironmentRelatedConfiguration();

            builder.RegisterServices(assemblies);
            builder.RegisterConfiguration(configuration, assemblies);
            builder.RegisterInstance(CreateGetApplicationConfiguration());
            builder.RegisterInstance(configuration).AsSelf().AsImplementedInterfaces();

            builder.RegisterInstance(Random.Shared);
            builder.RegisterModule(new AutoMapperModule(assemblies));
            builder.RegisterModule<ObjectStorageModule>();
            builder.RegisterType<ApplicationContext>();

            builder
                .RegisterType<MongoDbDataStorageProvider>()
                .Named<IDataStorageProvider>(DataStorage.Database)
                .AsImplementedInterfaces();
            builder
                .RegisterType<FileSystemDataStorageProvider>()
                .Named<IDataStorageProvider>(DataStorage.FileSystem)
                .AsImplementedInterfaces();

            return builder.Build();
        }
        private static RootConfig CreateGetApplicationConfiguration()
        {
            return new RootConfig()
            {
                NumberOfPersons = 100,
                FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Persons.json")
            };
        }
    }
}