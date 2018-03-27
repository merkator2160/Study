using SerializationTast.Interfaces;
using SerializationTast.Models;
using SerializationTast.Models.Config;
using SerializationTast.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationTast.Services
{
	internal class PersonCreatorService : IPersonCreatorService
	{
		private readonly Random _rnd;
		private readonly RootConfig _config;


		public PersonCreatorService(Random rnd, RootConfig config)
		{
			_rnd = rnd;
			_config = config;
		}


		// IPersonGeneratorService //////////////////////////////////////////////////////////////////////
		public Person[] Create()
		{
			return CreatePersons(_config.NumberOfPersons).ToArray();
		}


		// SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
		private IEnumerable<Person> CreatePersons(Int32 personsCount)
		{
			for(var i = 0; i < personsCount; i++)
			{
				yield return CreateRandomPerson(i);
			}
		}
		private Person CreateRandomPerson(Int32 sequence)
		{
			var personId = _rnd.Next();

			return new Person()
			{
				Id = personId,
				TransportId = Guid.NewGuid(),
				FirstName = $"First name of {personId}",
				LastName = $"Last name of {personId}",
				SequenceId = sequence,
				CreditCardNumbers = CreateCreditCards(_rnd.Next(10)).ToArray(),
				Age = _rnd.Next(100),
				Phones = CreatePhones(_rnd.Next(50)).ToArray(),
				BirthDate = (DateTime.UtcNow - TimeSpan.FromDays(_rnd.Next(10 * 365, 50 * 365))).ToBinary(),
				Salary = _rnd.Next(50000, 150000),
				IsMarred = _rnd.NextDouble() > 0.5,
				Gender = _rnd.NextDouble() > 0.5 ? Gender.Female : Gender.Male,
				Children = CreateChildren(_rnd.Next(4)).ToArray()
			};
		}
		private IEnumerable<String> CreateCreditCards(Int32 numberOfCreditCards)
		{
			for(var i = 0; i < numberOfCreditCards; i++)
			{
				yield return Guid.NewGuid().ToString().Replace("-", "");
			}
		}
		private IEnumerable<String> CreatePhones(Int32 numberOfPhones)
		{
			var stringBuilder = new StringBuilder(11);

			for(var i = 0; i < numberOfPhones; i++)
			{
				stringBuilder.Clear();
				stringBuilder.Append("+");
				for(var j = 0; j < 11; j++)
				{
					stringBuilder.Append(_rnd.Next(1, 9));
				}

				yield return stringBuilder.ToString();
			}
		}
		private IEnumerable<Child> CreateChildren(Int32 numberOfChildren)
		{
			for(var i = 0; i < numberOfChildren; i++)
			{
				var childId = _rnd.Next();
				yield return new Child()
				{
					Id = childId,
					FirstName = $"First child name of {childId}",
					LastName = $"Last child name of {childId}",
					BirthDate = (DateTime.UtcNow - TimeSpan.FromDays(_rnd.Next(1 * 365, 18 * 365))).ToBinary(),
					Gender = _rnd.NextDouble() > 0.5 ? Gender.Female : Gender.Male
				};
			}
		}
	}
}