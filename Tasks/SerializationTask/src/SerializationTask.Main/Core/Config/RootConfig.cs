using SerializationTask.Main.Database.Models.Config;
using System;

namespace SerializationTask.Main.Core.Config
{
	public class RootConfig
	{
		public Int32 NumberOfPersons { get; set; }
		public String FilePath { get; set; }
		public MongoDbConfig MongoConfig { get; set; }
	}
}