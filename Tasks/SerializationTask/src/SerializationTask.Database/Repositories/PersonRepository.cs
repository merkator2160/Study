using MongoDB.Driver;
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
		public IEnumerable<PersonDb> GetAll()
		{
			return _dataContext.Persons.Find(FilterDefinition<PersonDb>.Empty).ToCursor().ToEnumerable();
		}
		public void AddMany(IEnumerable<PersonDb> items)
		{
            // Consumes about of 1,5 Gb of memory per 1000000000 objects, not good //
            //_dataContext.Persons.InsertMany(items);		

            // Too slow //
            //foreach (var x in items)
            //{
            //    _dataContext.Persons.InsertOne(x);		
            //}

            // Optimal //
            const Int32 chunkSize = 50000;
            var tempList = new List<PersonDb>(chunkSize);
            foreach (var x in items)
			{
                tempList.Add(x);
                if (tempList.Count != chunkSize)
                    continue;

                _dataContext.Persons.InsertMany(tempList);
                tempList.Clear();
            }

            if (tempList.Count != 0)
                _dataContext.Persons.InsertMany(tempList);
        }
		public void CleanCollection()
		{
			_dataContext.Persons.DeleteMany(FilterDefinition<PersonDb>.Empty);
		}
	}
}