using CouchDB.Driver;
using CouchDB.Driver.Types;

namespace CouchDb.Client.Repositories
{
    public class CouchDbRepositoryBase<TEntity> where TEntity : CouchDocument
    {
        private readonly ICouchDatabase<TEntity> _database;


        public CouchDbRepositoryBase(ICouchDatabase<TEntity> database)
        {
            _database = database;
        }


        // IRepository ////////////////////////////////////////////////////////////////////////////
        public Int32 Count()
        {
            return _database.AsEnumerable().Count();
        }
        public Task<Int32> CountAsync()
        {
            return Task.Run(() => _database.AsEnumerable().Count());
        }
    }
}