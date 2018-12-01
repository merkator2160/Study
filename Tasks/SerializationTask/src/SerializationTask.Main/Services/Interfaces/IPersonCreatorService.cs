using SerializationTask.Main.Services.Models;

namespace SerializationTask.Main.Services.Interfaces
{
	internal interface IPersonCreatorService
	{
		PersonDto[] Create();
	}
}