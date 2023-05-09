using MongoDB.Driver;
using SerializationTask.Database.Models.Storage;

namespace SerializationTask.Database
{
    public class DataContext : DataContextBase
    {
        public DataContext(MongoUrlBuilder urlBuilder) : base(urlBuilder)
        {

        }


        // COLLECTIONS ////////////////////////////////////////////////////////////////////////////
        public IMongoCollection<PersonDb> Persons => Database.GetCollection<PersonDb>("Persons");
    }
}