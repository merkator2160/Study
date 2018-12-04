using Autofac;
using MongoDB.Driver;
using SerializationTask.Main.Core.AutoMapper;
using SerializationTask.Main.Core.Config;
using SerializationTask.Main.Database;
using SerializationTask.Main.Database.Models.Config;
using SerializationTask.Main.Services.DataProviders;
using System;
using System.IO;
using System.Reflection;

namespace SerializationTask.Main
{
	internal class Program
	{
		static void Main(String[] args)
		{
			using(var container = ConfigureContainer())
			{
				container.Resolve<ApplicationContext>().Run();
			}
		}


		// SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
		private static IContainer ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			var rootConfig = CreateGetApplicationConfiguration();
			var currentAssembly = Assembly.GetExecutingAssembly();

			builder.RegisterInstance(rootConfig);
			builder.RegisterInstance(rootConfig.MongoConfig);

			builder.RegisterType<ApplicationContext>();
			builder.RegisterInstance(new Random()).SingleInstance();
			builder.RegisterInstance(AutoMapperCreator.GetConfiguredMapper()).SingleInstance();
			builder.RegisterInstance(new MongoClient(rootConfig.MongoConfig.ConnectionString)).AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<DataContext>();

			builder
				.RegisterAssemblyTypes(currentAssembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces();

			builder
				.RegisterAssemblyTypes(currentAssembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces();

			builder.RegisterType<MongoDbDataStorageProvider>().AsImplementedInterfaces();
			//builder.RegisterType<FileSystemDataStorageProvider>().AsImplementedInterfaces();

			return builder.Build();
		}
		private static RootConfig CreateGetApplicationConfiguration()
		{
			return new RootConfig()
			{
				NumberOfPersons = 100,
				FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Persons.json"),
				MongoConfig = new MongoDbConfig()
				{
					ConnectionString = "mongodb://localhost:27017",
					DatabaseName = "SerializationTaskDb"
				}
			};
		}
	}
}