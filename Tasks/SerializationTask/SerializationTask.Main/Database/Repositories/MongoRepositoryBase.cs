using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Models;
using System;

namespace SerializationTask.Main.Database.Repositories
{
	internal class MongoRepositoryBase<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : MongoDataContext
	{
		private readonly MongoDataContext _context;


		public MongoRepositoryBase(TContext context)
		{
			_context = context;
		}


		// PROPERTIES /////////////////////////////////////////////////////////////////////////////
		protected TContext Context => _context as TContext;


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public PersonDb[] GetAll()
		{
			throw new NotImplementedException();
		}
		public TEntity Get(String id)
		{
			throw new NotImplementedException();
		}
		public void Create(TEntity item)
		{
			throw new NotImplementedException();
		}
		public void Update(TEntity item)
		{
			throw new NotImplementedException();
		}
		public void Delete(String id)
		{
			throw new NotImplementedException();
		}
	}
}