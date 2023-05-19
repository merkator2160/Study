using Autofac.Features.Indexed;
using SerializationTask.Services;
using SerializationTask.Services.Interfaces;
using SerializationTask.Services.Models;
using SerializationTask.Services.Models.Enums;

namespace SerializationTask
{
    internal class ApplicationContext
	{
		private readonly PersonCreatorService _personCreatorService;
        private readonly IIndex<DataStorage, IDataStorageProvider> _dataStorage;


        public ApplicationContext(PersonCreatorService personCreatorService, IIndex<DataStorage, IDataStorageProvider> dataStorage)
		{
			_personCreatorService = personCreatorService;
            _dataStorage = dataStorage;
        }


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            var dataStorageProvider = _dataStorage[DataStorage.Database];
			var personStream = _personCreatorService.Create();

			dataStorageProvider.Save(personStream);

			personStream = null;
			GC.Collect();

			personStream = dataStorageProvider.Restore();

			var result = CalculatePersonsCountCreditCardCountAndAverageChildAge(personStream);
			DisplayResult(result);

			Console.ReadKey();
		}
		private StepSixResultDto CalculatePersonsCountCreditCardCountAndAverageChildAge(IEnumerable<PersonDto> persons)
		{
			var personsCount = 0;
			var numberOfCreditCards = 0;
			var numberOfChildren = 0;
			var totalChildrenAge = 0;

			foreach(var x in persons)
			{
				personsCount++;
				numberOfCreditCards += x.CreditCardNumbers.Length;
				numberOfChildren += x.Children.Length;

				totalChildrenAge += x.Children.Sum(y => (DateTime.UtcNow - DateTime.FromBinary(y.BirthDate)).Days / 365);
			}

			return new StepSixResultDto()
			{
				PersonsCount = personsCount,
				NumberOfCreditCards = numberOfCreditCards,
				AverageChildAge = numberOfChildren == 0 ? 0 : totalChildrenAge / numberOfChildren
			};
		}
		private void DisplayResult(StepSixResultDto result)
		{
			Console.WriteLine($"{nameof(result.PersonsCount)}: {result.PersonsCount}");
			Console.WriteLine($"{nameof(result.NumberOfCreditCards)}: {result.NumberOfCreditCards}");
			Console.WriteLine($"{nameof(result.AverageChildAge)}: {result.AverageChildAge}");
		}
	}
}