using AutoMapper;
using SerializationTask.Main.Database.Models;
using SerializationTask.Main.Services.Models;

namespace SerializationTask.Main.Core.AutoMapper.Profiles
{
	public class PersonProfile : Profile
	{
		public PersonProfile()
		{
			CreateMap<PersonDto, PersonDb>()
				.ForMember(from => from.Id, to => to.Ignore())
				.ReverseMap();
		}
	}
}