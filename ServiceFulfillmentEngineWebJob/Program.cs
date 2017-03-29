using System;
using System.Configuration;
using System.Threading;

namespace ServiceFulfillmentEngineWebJob
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("sfe is started....");

			Timer t = new Timer(TimerCallback, null, 0, 5000);
			Console.ReadLine();
		}

		private static void TimerCallback(Object o)
		{
			Console.WriteLine("sfe cycle started");

			int userId = int.Parse(ConfigurationManager.AppSettings["FulfillmentUserId"]);
			string apiKey = ConfigurationManager.AppSettings["FulfillmentUserPrivateKey"];
			var manager = new FulfillmentManager(userId, apiKey);
			manager.FulfillNewRequests();
		}
	}
}
