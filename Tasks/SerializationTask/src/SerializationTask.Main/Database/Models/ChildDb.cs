using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SerializationTask.Main.Services.Models.Enums;
using System;

namespace SerializationTask.Main.Database.Models
{
	internal class ChildDb
	{
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }

		[BsonRepresentation(BsonType.String)]
		public Gender Gender { get; set; }
	}
}