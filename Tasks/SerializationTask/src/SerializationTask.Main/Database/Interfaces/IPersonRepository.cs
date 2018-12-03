using SerializationTask.Main.Database.Models;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IPersonRepository
	{
		PersonDb[] GetAll();
		void CleanCollection();
		void AddMany(PersonDb[] items);
	}
}