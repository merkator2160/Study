using Newtonsoft.Json;
using SerializationTast.Interfaces;
using SerializationTast.Models;
using SerializationTast.Models.Config;
using System.IO;
using System.Text;

namespace SerializationTast.Services
{
	internal class FileSystemDataStorageProvider : IDataStorageProvider
	{
		private readonly RootConfig _config;


		public FileSystemDataStorageProvider(RootConfig config)
		{
			_config = config;
		}


		// IDataStorageProvider ///////////////////////////////////////////////////////////////////
		public void Save(Person[] persons)
		{
			using(var stream = new FileStream(_config.FilePath, FileMode.Create, FileAccess.Write))
			{
				using(var streamWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					using(var textWriter = new JsonTextWriter(streamWriter))
					{
						new JsonSerializer().Serialize(textWriter, persons, typeof(Person[]));
					}
				}
			}
		}
		public Person[] Restore()
		{
			using(var stream = new FileStream(_config.FilePath, FileMode.Open, FileAccess.Read))
			{
				using(var streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					using(var jsonTextReader = new JsonTextReader(streamReader))
					{
						return new JsonSerializer().Deserialize<Person[]>(jsonTextReader);
					}
				}
			}
		}
	}
}