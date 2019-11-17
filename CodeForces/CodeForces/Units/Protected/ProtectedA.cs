using System;

namespace CodeForces.Units.Protected
{
	internal class ProtectedA
	{
		public void Execute()
		{
			Console.WriteLine($"{nameof(ProtectedA)} executing: {nameof(ProtectedA.Execute)}");

			ExecuteProtected();
		}
		protected void ExecuteProtected()
		{
			Console.WriteLine($"{nameof(ProtectedA)} executing: {nameof(ProtectedA.ExecuteProtected)}");
		}
	}
}