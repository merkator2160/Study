using SerializationTask.Main.Config.AutoMapper;
using SerializationTask.Main.Config.Models;
using SerializationTask.Main.Database;
using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Repositories;
using SerializationTask.Main.Services;
using SerializationTask.Main.Services.DataProviders;
using SerializationTask.Main.Services.Interfaces;
using System;
using System.IO;
using Unity;

namespace SerializationTask.Main
{
	internal class Program
	{
		static void Main(String[] args)
		{
			var container = ConfigureContainer();

			//var dataProvider = container.Resolve<MongoDataContext>();
			//foreach (var x in dataProvider.GetAllCollections())
			//{
			//	Console.WriteLine(x);
			//}

			//Console.ReadKey();

			container.Resolve<ApplicationContext>().Run();
		}


		// SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
		private static IUnityContainer ConfigureContainer()
		{
			var container = new UnityContainer();

			container.RegisterInstance(new Random());
			container.RegisterInstance(AutoMapperCreator.GetConfiguredMapper());
			container.RegisterInstance(GetApplicationConfiguration());

			container.RegisterType<ApplicationContext>();
			container.RegisterType<MongoDataContext>();

			container.RegisterType<IPersonRepository, PersonRepository>();
			container.RegisterType<IPersonCreatorService, PersonCreatorService>();
			container.RegisterType<IDataStorageProvider, MongoDbDataStorageProvider>();
			//container.RegisterType<IDataStorageProvider, FileSystemDataStorageProvider>();

			return container;
		}
		private static RootConfig GetApplicationConfiguration()
		{
			return new RootConfig()
			{
				NumberOfPersons = 100,
				FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Persons.json"),
				MongoConfig = new MongoDbConfig()
				{
					ConnectionString = "mongodb://localhost:27017/PersonsDb"
				}
			};
		}
	}
}