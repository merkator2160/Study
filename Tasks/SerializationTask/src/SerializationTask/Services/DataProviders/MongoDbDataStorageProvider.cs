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
		public void Save(IEnumerable<PersonDto> persons)
		{
            _personRepository.CleanCollection();
            _personRepository.AddMany(persons.Select(x => _mapper.Map<PersonDb>(x)));
		}
		public IEnumerable<PersonDto> Restore()
		{
			return _personRepository.GetAll().Select(x => _mapper.Map<PersonDto>(x));
		}
    }
}