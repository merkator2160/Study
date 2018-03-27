using SerializationTast.Models.Enums;
using System;

namespace SerializationTast.Models
{
	internal class Child
	{
		public Int32 Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }
		public Gender Gender { get; set; }
	}
}