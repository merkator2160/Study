using System;
using System.Collections.Generic;

namespace CinemaSchedule.DataBase.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Int32 id);
        IReadOnlyCollection<TEntity> GetAll();
        TEntity Add(TEntity item);
        IReadOnlyCollection<TEntity> AddRange(IReadOnlyCollection<TEntity> items);
        void Update(TEntity item);
        void Delete(Int32 id);
        void Remove(TEntity item);
        void RemoveRange(IReadOnlyCollection<TEntity> items);
    }
}