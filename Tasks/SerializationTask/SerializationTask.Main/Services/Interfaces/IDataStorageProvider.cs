using SerializationTask.Main.Services.Models;

namespace SerializationTask.Main.Services.Interfaces
{
	internal interface IDataStorageProvider
	{
		void Save(PersonDto[] persons);
		PersonDto[] Restore();
	}
}