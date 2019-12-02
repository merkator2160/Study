using System.Threading.Tasks;

namespace Common.Hangfire.Interfaces
{
	public interface IJob
	{
		Task ExecuteAsync();
	}
}