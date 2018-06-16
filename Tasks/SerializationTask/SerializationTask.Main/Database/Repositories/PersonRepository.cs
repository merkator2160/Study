using MongoDB.Bson;
using MongoDB.Driver;
using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Models;
using System;

namespace SerializationTask.Main.Database.Repositories
{
	internal class PersonRepository : IPersonRepository
	{
		private readonly MongoDataContext _dbContext;


		public PersonRepository(MongoDataContext dbContext)
		{
			_dbContext = dbContext;
		}


		// IRepository ////////////////////////////////////////////////////////////////////////////
		public PersonDb[] GetAll()
		{
			return _dbContext.Persons.Find(new BsonDocument()).ToList().ToArray();
		}
		public PersonDb Get(String id)
		{
			return _dbContext.Persons.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefault();
		}
		public void Create(PersonDb item)
		{
			_dbContext.Persons.InsertOne(item);
		}
		public void Update(PersonDb item)
		{
			_dbContext.Persons.ReplaceOne(new BsonDocument("_id", item.Id), item);
		}
		public void Delete(String id)
		{
			_dbContext.Persons.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
		}


		// IPersonRepository //////////////////////////////////////////////////////////////////////
		public void BulkUpdate(PersonDb[] items)
		{
			foreach (var x in items)
			{
				_dbContext.Persons.ReplaceOne(new BsonDocument("_id", x.Id), x, new UpdateOptions()
				{
					IsUpsert = true
				});
			}
		}
		public void CleanCollection()
		{
			_dbContext.Persons.DeleteMany(new BsonDocument());
		}
		public void BulkInsert(PersonDb[] items)
		{
			_dbContext.Persons.InsertMany(items);
		}
	}
}