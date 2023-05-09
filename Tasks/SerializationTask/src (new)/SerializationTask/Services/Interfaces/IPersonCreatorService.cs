using SerializationTask.Services.Models;

namespace SerializationTask.Services.Interfaces
{
    internal interface IPersonCreatorService
	{
		PersonDto[] Create();
	}
}