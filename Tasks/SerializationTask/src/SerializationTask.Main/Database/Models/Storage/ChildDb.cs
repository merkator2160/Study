using SerializationTask.Main.Services.Models.Enums;
using System;

namespace SerializationTask.Main.Database.Models.Storage
{
	internal class ChildDb
	{
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }
		public Gender Gender { get; set; }
	}
}