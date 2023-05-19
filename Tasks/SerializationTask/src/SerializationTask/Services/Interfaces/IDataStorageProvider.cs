using SerializationTask.Services.Models;

namespace SerializationTask.Services.Interfaces
{
    internal interface IDataStorageProvider
	{
		void Save(IEnumerable<PersonDto> persons);
        IEnumerable<PersonDto> Restore();
	}
}