using SerializationTask.Main.Database.Models;
using System;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IRepository<TEntity> where TEntity : class
	{
		PersonDb[] GetAll();
		TEntity Get(String id);
		void Add(TEntity item);
		void Update(TEntity item);
		void Delete(String id);
	}
}