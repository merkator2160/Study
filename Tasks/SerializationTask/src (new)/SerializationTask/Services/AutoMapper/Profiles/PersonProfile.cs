using AutoMapper;
using SerializationTask.Database.Models.Storage;
using SerializationTask.Services.Models;

namespace SerializationTask.Services.AutoMapper.Profiles
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