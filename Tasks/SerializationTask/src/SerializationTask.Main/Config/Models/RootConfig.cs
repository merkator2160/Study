using System;

namespace SerializationTask.Main.Config.Models
{
	public class RootConfig
	{
		public Int32 NumberOfPersons { get; set; }
		public String FilePath { get; set; }
		public MongoDbConfig MongoConfig { get; set; }
	}
}