using AutoMapper;
using SerializationTask.Main.Database.Interfaces;
using SerializationTask.Main.Database.Models;
using SerializationTask.Main.Services.Interfaces;
using SerializationTask.Main.Services.Models;

namespace SerializationTask.Main.Services.DataProviders
{
	internal class MongoDbDataStorageProvider : IDataStorageProvider
	{
		private readonly IMapper _mapper;
		private readonly IPersonRepository _personRepository;


		public MongoDbDataStorageProvider(IMapper mapper, IPersonRepository personRepository)
		{
			_mapper = mapper;
			_personRepository = personRepository;
		}

		
		// IDataStorageProvider ///////////////////////////////////////////////////////////////////
		public void Save(PersonDto[] persons)
		{
			var personsDb = _mapper.Map<PersonDb[]>(persons);

			_personRepository.CleanCollection();
			_personRepository.BulkInsert(personsDb);
		}
		public PersonDto[] Restore()
		{
			var personsDb = _personRepository.GetAll();
			var persons = _mapper.Map<PersonDto[]>(personsDb);

			return persons;
		}
	}
}