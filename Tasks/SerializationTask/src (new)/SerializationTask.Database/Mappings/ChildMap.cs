using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SerializationTask.Common.Contracts.Enums;
using SerializationTask.Database.Models.Storage;

namespace SerializationTask.Database.Mappings
{
    internal class ChildMap : BsonClassMap<ChildDb>
	{
		public ChildMap()
		{
			AutoMap();
            MapMember(p => p.Gender).SetSerializer(new EnumSerializer<Gender>(BsonType.String));
		}
	}
}