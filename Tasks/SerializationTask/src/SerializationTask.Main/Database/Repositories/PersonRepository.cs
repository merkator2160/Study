using MongoDB.Bson;
using MongoDB.Driver;
using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Models.Storage;

namespace SerializationTask.Main.Database.Repositories
{
	internal class PersonRepository : IPersonRepository
	{
		private readonly DataContext _dataContext;


		public PersonRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}


		// IPersonRepository //////////////////////////////////////////////////////////////////////
		public PersonDb[] GetAll()
		{
			return _dataContext.Persons.Find(new BsonDocument()).ToList().ToArray();
		}
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