using SerializationTast.Interfaces;
using SerializationTast.Models.Config;
using SerializationTast.Services;
using System;
using System.IO;
using Unity;

namespace SerializationTast
{
	internal class Program
	{
		static void Main(String[] args)
		{
			var container = ConfigureContainer();
			container.Resolve<ApplicationContext>().Run();
		}


		// SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
		private static IUnityContainer ConfigureContainer()
		{
			var container = new UnityContainer();

			container.RegisterInstance(new Random());
			container.RegisterType<IPersonCreatorService, PersonCreatorService>();
			container.RegisterType<IDataStorageProvider, FileSystemDataStorageProvider>();
			container.RegisterType<ApplicationContext>();
			container.RegisterInstance(new RootConfig()
			{
				NumberOfPersons = 100,
				FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Persons.json")
			});

			return container;
		}
	}
}