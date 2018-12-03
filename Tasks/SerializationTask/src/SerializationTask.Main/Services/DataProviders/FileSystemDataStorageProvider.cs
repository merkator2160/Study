using Newtonsoft.Json;
using SerializationTask.Main.Config.Config;
using SerializationTask.Main.Services.Interfaces;
using SerializationTask.Main.Services.Models;
using System.IO;
using System.Text;

namespace SerializationTask.Main.Services.DataProviders
{
	internal class FileSystemDataStorageProvider : IDataStorageProvider
	{
		private readonly RootConfig _config;


		public FileSystemDataStorageProvider(RootConfig config)
		{
			_config = config;
		}


		// IDataStorageProvider ///////////////////////////////////////////////////////////////////
		public void Save(PersonDto[] persons)
		{
			using(var stream = new FileStream(_config.FilePath, FileMode.Create, FileAccess.Write))
			{
				using(var streamWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					using(var textWriter = new JsonTextWriter(streamWriter))
					{
						new JsonSerializer().Serialize(textWriter, persons, typeof(PersonDto[]));
					}
				}
			}
		}
		public PersonDto[] Restore()
		{
			using(var stream = new FileStream(_config.FilePath, FileMode.Open, FileAccess.Read))
			{
				using(var streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					using(var jsonTextReader = new JsonTextReader(streamReader))
					{
						return new JsonSerializer().Deserialize<PersonDto[]>(jsonTextReader);
					}
				}
			}
		}
	}
}