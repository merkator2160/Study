using ApiClients.Bokun;
using ApiClients.StackDriver.Models.Config;
using System;

namespace Sandbox.Units
{
	internal static class BokunApiUnit
	{
		public static void Run()
		{
			using(var client = new BokunClient(new BokunClientConfig()
			{
				BaseUrl = "https://api.bokuntest.com",
				AccessKey = "08f1115a8c5d4edcb662e369a9a1747c",
				//AccessKey = "de235a6a15c340b6b1e1cb5f3687d04a",
				SecretKey = "a07b25ce4c604471a35a406c085b546d"
			}))
			{
				//var result = client.ActivitySearchAsync().GetAwaiter().GetResult();
				var result = client.CurrencyFindAllAsync().GetAwaiter().GetResult();

				Console.WriteLine(result);
			}

			Console.ReadKey();
		}
	}
}