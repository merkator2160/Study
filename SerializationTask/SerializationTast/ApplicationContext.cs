using SerializationTast.Interfaces;
using SerializationTast.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializationTast
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

			var result = CalculateStepSix(persons);
			DisplayResult(result);

			Console.ReadKey();
		}
		private static StepSixResult CalculateStepSix(IReadOnlyCollection<Person> persons)
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

			return new StepSixResult()
			{
				PersonsCount = personsCount,
				NumberOfCreditCards = numberOfCreditCards,
				AverageChildAge = totalChildrenAge / numberOfChildren
			};
		}
		private static void DisplayResult(StepSixResult result)
		{
			Console.WriteLine($"{nameof(result.PersonsCount)}: {result.PersonsCount}");
			Console.WriteLine($"{nameof(result.NumberOfCreditCards)}: {result.NumberOfCreditCards}");
			Console.WriteLine($"{nameof(result.AverageChildAge)}: {result.AverageChildAge}");
		}
	}
}