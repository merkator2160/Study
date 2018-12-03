using System;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IRepository<TEntity> where TEntity : class
	{
		TEntity[] GetAll();
		TEntity Get(String id);
		void Add(TEntity item);
		void Update(TEntity item);
		void Delete(String id);
	}
}