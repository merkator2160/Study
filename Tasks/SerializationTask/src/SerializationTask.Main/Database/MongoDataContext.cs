using MongoDB.Driver;
using SerializationTask.Main.Config.Models;
using SerializationTask.Main.Database.Models;
using System;
using System.Linq;

namespace SerializationTask.Main.Database
{
	internal class MongoDataContext
	{
		private readonly MongoDbConfig _config;
		private readonly IMongoClient _dbClient;
		private readonly IMongoDatabase _dataBase;


		public MongoDataContext(IMongoClient dbClient, MongoDbConfig config)
		{
			_config = config;
			_dbClient = dbClient;
			_dataBase = _dbClient.GetDatabase(config.DatabaseName);
		}


		// COLLECTIONS ////////////////////////////////////////////////////////////////////////////
		public IMongoCollection<PersonDb> Persons => _dataBase.GetCollection<PersonDb>("Persons");


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public String[] GetAllCollections()
		{
			using(var collections = _dataBase.ListCollections())
			{
				var collectionList = collections.ToList();
				return collectionList.Select(x => x["name"].ToString()).ToArray();
			}
		}
	}
}