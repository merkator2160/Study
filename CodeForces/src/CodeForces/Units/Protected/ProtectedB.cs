using System;

namespace CodeForces.Units.Protected
{
	internal class ProtectedB : ProtectedA
	{
		public new void Execute()
		{
			Console.WriteLine($"{nameof(ProtectedB)} executing: {nameof(ProtectedB.Execute)}");

			ExecuteProtected();
		}
	}
}