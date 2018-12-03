using MongoDB.Bson;
using MongoDB.Driver;
using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Models;
using System;

namespace SerializationTask.Main.Database.Repositories
{
	internal class PersonRepository : IPersonRepository
	{
		private readonly MongoDataContext _dataContext;


		public PersonRepository(MongoDataContext dataContext)
		{
			_dataContext = dataContext;
		}


		// IRepository ////////////////////////////////////////////////////////////////////////////
		public PersonDb[] GetAll()
		{
			return _dataContext.Persons.Find(new BsonDocument()).ToList().ToArray();
		}
		public PersonDb Get(String id)
		{
			return _dataContext.Persons.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefault();
		}
		public void Add(PersonDb item)
		{
			_dataContext.Persons.InsertOne(item);
		}
		public void Update(PersonDb item)
		{
			_dataContext.Persons.ReplaceOne(new BsonDocument("_id", item.Id), item);
		}
		public void Delete(String id)
		{
			_dataContext.Persons.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
		}


		// IPersonRepository //////////////////////////////////////////////////////////////////////
		public void AddMany(PersonDb[] items)
		{
			_dataContext.Persons.InsertMany(items);
		}
		public void CleanCollection()
		{
			_dataContext.Persons.DeleteMany(new BsonDocument());
		}
	}
}