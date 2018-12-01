using SerializationTask.Main.Database.Models;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IPersonRepository : IRepository<PersonDb>
	{
		void BulkUpdate(PersonDb[] items);
		void CleanCollection();
		void BulkInsert(PersonDb[] items);
	}
}