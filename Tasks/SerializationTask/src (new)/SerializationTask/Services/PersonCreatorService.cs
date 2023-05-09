using SerializationTask.Common.Contracts.Enums;
using SerializationTask.Common.Helpers;
using SerializationTask.Common.Tools;
using SerializationTask.Services.Models;
using SerializationTask.Services.Models.Config;
using System.Text;

namespace SerializationTask.Services
{
    internal class PersonCreatorService
	{
		private readonly Random _rnd;
		private readonly RootConfig _config;


		public PersonCreatorService(Random rnd, RootConfig config)
		{
			_rnd = rnd;
			_config = config;
		}


		// IPersonGeneratorService //////////////////////////////////////////////////////////////////////
		public PersonDto[] Create()
		{
			return CreatePersons(_config.NumberOfPersons).ToArray();
		}


		// SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
		private IEnumerable<PersonDto> CreatePersons(Int32 personsCount)
		{
			for(var i = 0; i < personsCount; i++)
			{
				yield return CreateRandomPerson(i);
			}
		}
		private PersonDto CreateRandomPerson(Int32 sequence)
		{
			var personId = Guid.NewGuid();

			return new PersonDto()
			{
				TransportId = personId,
				FirstName = $"First name of {personId}",
				LastName = $"Last name of {personId}",
				SequenceId = sequence,
				CreditCardNumbers = RandomCreditCardNumberGenerator.GenerateMasterCardNumbers(_rnd.Next(10)).ToArray(),
				Phones = CreatePhones(_rnd.Next(50)).ToArray(),
				BirthDate = (DateTime.UtcNow - TimeSpan.FromDays(_rnd.Next(10 * 365, 50 * 365))).ToPosixTimeSec(),
				Salary = _rnd.Next(50000, 150000),
				IsMarred = _rnd.NextDouble() > 0.5,
				Gender = _rnd.NextDouble() > 0.5 ? Gender.Female : Gender.Male,
				Children = CreateChildren(_rnd.Next(4)).ToArray()
			};
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
					stringBuilder.Append(_rnd.Next(0, 9));
				}

				yield return stringBuilder.ToString();
			}
		}
		private IEnumerable<ChildDto> CreateChildren(Int32 numberOfChildren)
		{
			for(var i = 0; i < numberOfChildren; i++)
			{
				var childId = Guid.NewGuid();
				yield return new ChildDto()
				{
					FirstName = $"First child name of {childId}",
					LastName = $"Last child name of {childId}",
					BirthDate = (DateTime.UtcNow - TimeSpan.FromDays(_rnd.Next(1 * 365, 18 * 365))).ToBinary(),
					Gender = _rnd.NextDouble() > 0.5 ? Gender.Female : Gender.Male
				};
			}
		}
	}
}