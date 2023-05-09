using MongoDB.Driver;
using SerializationTask.Database.Extensions;
using SerializationTask.Database.Models.Storage;

namespace SerializationTask.Database.Repositories
{
    public class PersonRepository
	{
		private readonly DataContext _dataContext;


		public PersonRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}


		// IPersonRepository //////////////////////////////////////////////////////////////////////
		public PersonDb[] GetAll()
		{
			return _dataContext.Persons.Find(FilterDefinition<PersonDb>.Empty).ToArray();
		}
		public void AddMany(PersonDb[] items)
		{
			_dataContext.Persons.InsertMany(items);
		}
		public void CleanCollection()
		{
			_dataContext.Persons.DeleteMany(FilterDefinition<PersonDb>.Empty);
		}
	}
}