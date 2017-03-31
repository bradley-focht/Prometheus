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
			Timer timer = new Timer(TimerCallback, null, 0, 10000);

			Console.WriteLine("SFE has started....");
			Console.ReadLine();
		}

		private static void TimerCallback(Object o)
		{
			string username = ConfigurationManager.AppSettings["FulfillmentUserUsername"];
			string password = ConfigurationManager.AppSettings["FulfillmentUserPassword"];
			var manager = new FulfillmentManager(username, password);
			manager.FulfillNewRequests();
		}
	}
}
