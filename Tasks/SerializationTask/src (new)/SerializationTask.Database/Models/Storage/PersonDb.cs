using MongoDB.Bson;
using SerializationTask.Common.Contracts.Enums;

namespace SerializationTask.Database.Models.Storage
{
    public class PersonDb
	{
		public ObjectId Id { get; set; }
		public Guid TransportId { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int32 SequenceId { get; set; }
		public String[] CreditCardNumbers { get; set; }
		public String[] Phones { get; set; }
		public Int64 BirthDate { get; set; }
		public Double Salary { get; set; }
		public Boolean IsMarred { get; set; }
		public Gender Gender { get; set; }
		public ChildDb[] Children { get; set; }
	}
}