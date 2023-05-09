using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using SerializationTask.Common.Contracts.Enums;
using SerializationTask.Database.Models.Storage;

namespace SerializationTask.Database.Mappings
{
    internal class PersonMap : BsonClassMap<PersonDb>
	{
		public PersonMap()
		{
			AutoMap();
			MapIdMember(c => c.Id).SetIdGenerator(new ObjectIdGenerator());
			MapMember(p => p.Gender).SetSerializer(new EnumSerializer<Gender>(BsonType.String));
			MapMember(p => p.Children).SetIgnoreIfNull(true);
		}
	}
}