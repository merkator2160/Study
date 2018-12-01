using SerializationTask.Main.Services.Models.Enums;
using System;

namespace SerializationTask.Main.Services.Models
{
	internal class ChildDto
	{
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }
		public Gender Gender { get; set; }
	}
}