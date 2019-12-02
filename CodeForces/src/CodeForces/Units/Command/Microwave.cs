using System;
using System.Threading.Tasks;

namespace CodeForces.Units.Command
{
	public class Microwave
	{
		public void StartCooking(Int32 time)
		{
			Console.WriteLine("Подогреваем еду");
			Task.Delay(time).GetAwaiter().GetResult();  // имитация работы с помощью асинхронного метода Task.Delay
		}

		public void StopCooking()
		{
			Console.WriteLine("Еда подогрета!");
		}
	}
}