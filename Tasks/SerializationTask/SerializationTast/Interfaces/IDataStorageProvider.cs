using SerializationTast.Models;

namespace SerializationTast.Interfaces
{
	internal interface IDataStorageProvider
	{
		void Save(Person[] persons);
		Person[] Restore();
	}
}