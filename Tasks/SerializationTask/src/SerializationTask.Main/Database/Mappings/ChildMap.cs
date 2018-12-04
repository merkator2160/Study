using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SerializationTask.Main.Database.Models.Storage;
using SerializationTask.Main.Services.Models.Enums;

namespace SerializationTask.Main.Database.Mappings
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