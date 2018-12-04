using SerializationTask.Main.Database.Models.Storage;

namespace SerializationTask.Main.Database.Interfaces
{
	internal interface IPersonRepository
	{
		PersonDb[] GetAll();
		void CleanCollection();
		void AddMany(PersonDb[] items);
	}
}