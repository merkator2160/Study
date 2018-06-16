using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerializationTask.Main.Services.Models.Enums;
using System;

namespace SerializationTask.Main.Database.Models
{
	internal class ChildDb
	{
		public ObjectId Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }

		[BsonRepresentation(BsonType.Int32)]
		public Gender Gender { get; set; }
	}
}