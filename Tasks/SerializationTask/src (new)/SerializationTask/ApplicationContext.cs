using Autofac;
using SerializationTask.Common.Contracts.Const;
using SerializationTask.Services.Interfaces;
using SerializationTask.Services.Models;

namespace SerializationTask
{
    internal class ApplicationContext
	{
		private readonly IPersonCreatorService _personCreatorService;
        private readonly ILifetimeScope _lifetimeScope;


        public ApplicationContext(IPersonCreatorService personCreatorService, ILifetimeScope lifetimeScope)
		{
			_personCreatorService = personCreatorService;
            _lifetimeScope = lifetimeScope;
        }


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public void Run()
		{
            var dataStorageProvider = _lifetimeScope.ResolveNamed<IDataStorageProvider>(DataStorage.Database);
            var persons = _personCreatorService.Create();

			dataStorageProvider.Save(persons);

			persons = null;

            persons = dataStorageProvider.Restore();

			var result = CalculatePersonsCountCreditCardCountAndAverageChildAge(persons);
			DisplayResult(result);

			Console.ReadKey();
		}
		private StepSixResultDto CalculatePersonsCountCreditCardCountAndAverageChildAge(IReadOnlyCollection<PersonDto> persons)
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
				AverageChildAge = totalChildrenAge / numberOfChildren
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