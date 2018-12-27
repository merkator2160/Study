using System;

namespace CodeForces.Units
{
	public static class SumUnit
	{
		public static void Run()
		{
			Console.WriteLine(SumV1(100));
			Console.WriteLine(SumV2(100));
		}
		private static Int32 SumV1(Int32 n)
		{
			if(n <= 1)
				return 1;

			return n + SumV1(n - 1);
		}
		private static Int32 SumV2(Int32 n)
		{
			return (n * (n + 1)) / 2;
		}
	}
}