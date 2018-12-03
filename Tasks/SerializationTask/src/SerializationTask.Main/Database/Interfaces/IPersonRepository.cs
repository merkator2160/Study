using SerializationTask.Main.Database.Models;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IPersonRepository : IRepository<PersonDb>
	{
		void CleanCollection();
		void AddMany(PersonDb[] items);
	}
}