using SerializationTask.Main.Services.Interfaces;
using SerializationTask.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializationTask.Main
{
	internal class ApplicationContext
	{
		private readonly IPersonCreatorService _personCreatorService;
		private readonly IDataStorageProvider _dataStorageProvider;


		public ApplicationContext(IPersonCreatorService personCreatorService, IDataStorageProvider dataStorageProvider)
		{
			_personCreatorService = personCreatorService;
			_dataStorageProvider = dataStorageProvider;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public void Run()
		{
			var persons = _personCreatorService.Create();

			_dataStorageProvider.Save(persons);

			persons = null;

			persons = _dataStorageProvider.Restore();

			var result = CalculatePersonsCountCreditCardCountAndAvarageChildAge(persons);
			DisplayResult(result);

			Console.ReadKey();
		}
		private StepSixResultDto CalculatePersonsCountCreditCardCountAndAvarageChildAge(IReadOnlyCollection<PersonDto> persons)
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