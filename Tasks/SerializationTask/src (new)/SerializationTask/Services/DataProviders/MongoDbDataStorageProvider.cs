using AutoMapper;
using SerializationTask.Database.Models.Storage;
using SerializationTask.Database.Repositories;
using SerializationTask.Services.Interfaces;
using SerializationTask.Services.Models;

namespace SerializationTask.Services.DataProviders
{
    internal class MongoDbDataStorageProvider : IDataStorageProvider
	{
		private readonly IMapper _mapper;
		private readonly PersonRepository _personRepository;


		public MongoDbDataStorageProvider(IMapper mapper, PersonRepository personRepository)
		{
			_mapper = mapper;
			_personRepository = personRepository;
		}


		// IDataStorageProvider ///////////////////////////////////////////////////////////////////
		public void Save(PersonDto[] persons)
		{
			var personsDb = _mapper.Map<PersonDb[]>(persons);

			_personRepository.CleanCollection();
			_personRepository.AddMany(personsDb);
		}
		public PersonDto[] Restore()
		{
			var personsDb = _personRepository.GetAll();
			var persons = _mapper.Map<PersonDto[]>(personsDb);

			return persons;
		}
	}
}