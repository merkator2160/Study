using System;

namespace CodeForces.Units.Protected
{
	internal static class ProtectedUnit
	{
		public static void Run()
		{
			var a = new ProtectedA();
			a.Execute();

			var b = new ProtectedB();
			b.Execute();

			Console.ReadKey();
		}
	}
}
