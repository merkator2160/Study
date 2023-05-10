using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Reflection;

namespace SerializationTask.Database
{
    public abstract class DataContextBase
    {
        static DataContextBase()
        {
            RegisterMappings(Assembly.GetExecutingAssembly());
        }
        protected DataContextBase(MongoUrlBuilder urlBuilder)
        {
            Client = new MongoClient(urlBuilder.ToMongoUrl());
            Database = Client.GetDatabase(urlBuilder.DatabaseName);
        }


        // SERVER /////////////////////////////////////////////////////////////////////////////////
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void Ping()
        {
            using (var cancellationTokenSource = new CancellationTokenSource(10 * 1000))
            {
                var result = Database.RunCommand<BsonDocument>("{ping:1}", null, cancellationTokenSource.Token);
            }
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        protected static void RegisterMappings(Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => IsSubclassOfRawGeneric(typeof(BsonClassMap<>), type)).ToArray()
                .ToArray();

            foreach (var x in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(x);
                BsonClassMap.RegisterClassMap(configurationInstance);
            }
        }
        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}