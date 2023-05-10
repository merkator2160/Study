using SerializationTask.Services.Models;

namespace SerializationTask.Services.Interfaces
{
    internal interface IDataStorageProvider
	{
		void Save(PersonDto[] persons);
		PersonDto[] Restore();
	}
}