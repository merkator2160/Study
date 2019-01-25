using System;

namespace CodeForces.Units
{
	public static class SumUnit
	{
		public static void Run()
		{
			Console.WriteLine(SumV1(100));
			Console.WriteLine(SumV2(100));
			Console.WriteLine(SumV3(100));
		}
		private static Int32 SumV1(Int32 n)
		{
			var result = 0;
			for(var i = 0; i <= n; i++)
			{
				result = result + i;
			}

			return result;
		}
		private static Int32 SumV2(Int32 n)
		{
			if(n <= 1)
				return 1;

			return n + SumV2(n - 1);
		}
		private static Int32 SumV3(Int32 n)
		{
			return (n * (n + 1)) / 2;
		}
	}
}