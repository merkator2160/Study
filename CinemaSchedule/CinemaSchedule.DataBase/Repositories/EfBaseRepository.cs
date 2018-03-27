using CinemaSchedule.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CinemaSchedule.DataBase.Repositories
{
    public abstract class EfBaseRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly DbContext _context;


        protected EfBaseRepository(DbContext context)
        {
            _context = context;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        protected TContext Context => _context as TContext;


        // IRepository<TEntity> //////////////////////////////////////////////////////////////////
        public virtual TEntity Get(Int32 id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public virtual IReadOnlyCollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToArray();
        }
        public virtual TEntity Add(TEntity item)
        {
            return _context.Set<TEntity>().Add(item);
        }
        public virtual IReadOnlyCollection<TEntity> AddRange(IReadOnlyCollection<TEntity> items)
        {
            return _context.Set<TEntity>().AddRange(items).ToArray();
        }
        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public virtual void Delete(Int32 id)
        {
            var item = _context.Set<TEntity>().Find(id);
            if (item != null)
                _context.Set<TEntity>().Remove(item);
        }
        public virtual void Remove(TEntity item)
        {
            _context.Set<TEntity>().Remove(item);
        }
        public virtual void RemoveRange(IReadOnlyCollection<TEntity> items)
        {
            _context.Set<TEntity>().RemoveRange(items);
        }
    }
}