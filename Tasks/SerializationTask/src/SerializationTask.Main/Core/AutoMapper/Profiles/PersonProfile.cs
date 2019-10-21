using AutoMapper;
using SerializationTask.Main.Database.Models.Storage;
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

			CreateMap<ChildDto, ChildDb>()
				.ReverseMap();
		}
	}
}