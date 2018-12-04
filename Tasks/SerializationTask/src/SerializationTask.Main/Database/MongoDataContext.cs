using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SerializationTask.Main.Database.Models.Config;
using SerializationTask.Main.Database.Models.Storage;
using System;
using System.Linq;
using System.Reflection;

namespace SerializationTask.Main.Database
{
	internal class MongoDataContext
	{
		private readonly MongoDbConfig _config;
		private readonly IMongoClient _dbClient;
		private readonly IMongoDatabase _dataBase;


		static MongoDataContext()
		{
			RegisterMappings();
		}
		public MongoDataContext(IMongoClient dbClient, MongoDbConfig config)
		{
			_config = config;
			_dbClient = dbClient;
			_dataBase = _dbClient.GetDatabase(config.DatabaseName);
		}


		// COLLECTIONS ////////////////////////////////////////////////////////////////////////////
		public IMongoCollection<PersonDb> Persons => _dataBase.GetCollection<PersonDb>("Persons");


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		private static void RegisterMappings()
		{
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => !String.IsNullOrEmpty(type.Namespace))
				.Where(type => IsSubclassOfRawGeneric(typeof(BsonClassMap<>), type)).ToArray()
				.ToArray();

			foreach(var x in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(x);
				BsonClassMap.RegisterClassMap(configurationInstance);
			}
		}
		private static Boolean IsSubclassOfRawGeneric(Type generic, Type toCheck)
		{
			while(toCheck != null && toCheck != typeof(Object))
			{
				var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if(generic == cur)
					return true;
				toCheck = toCheck.BaseType;
			}
			return false;
		}
	}
}