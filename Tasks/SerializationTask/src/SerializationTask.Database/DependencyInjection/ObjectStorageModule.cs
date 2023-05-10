using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Reflection;
using Module = Autofac.Module;

namespace SerializationTask.Database.DependencyInjection
{
    public class ObjectStorageModule : Module
    {
        private readonly Assembly _currentAssembly;


        public ObjectStorageModule()
        {
            _currentAssembly = Assembly.GetExecutingAssembly();
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public const String SerializationTaskDbConnectionStringName = "SerializationTaskDbConnection";


        // COMPONENT REGISTRATION /////////////////////////////////////////////////////////////////

        /// <summary>
        /// It is recommended to store a MongoClient instance in a global place, either as a static variable or in an IoC container with a singleton lifetime.
        /// https://stackoverflow.com/questions/59599151/how-should-i-register-my-mongodb-service-that-uses-the-mongoclient-singleton-or 
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(scope =>
            {
                var connectionString = scope.Resolve<IConfiguration>().GetConnectionString(SerializationTaskDbConnectionStringName);
                return new DataContext(new MongoUrlBuilder(connectionString));
            }).AsSelf().SingleInstance();

            RegisterRepositories(builder, _currentAssembly);
        }
        private void RegisterRepositories(ContainerBuilder builder, Assembly assembly)
        {
            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsSelf();
        }
    }
}