using System;

namespace CodeForces.Units.Interview
{
	public static class PerformanceTasks
	{
		public static void Run()
		{
			var reversedStr = ReverseString("Lorem Ipsum is simply dummy text of the printing and typesetting industry.");
			Console.WriteLine(reversedStr);
		}


		// TASKS //////////////////////////////////////////////////////////////////////////////////
		public static String ReverseString(String str)
		{
			var charArray = str.ToCharArray();
			Array.Reverse(charArray);

			return new String(charArray);
		}
	}
}