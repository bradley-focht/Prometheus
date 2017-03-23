using System.Configuration;
using System.Threading;

namespace ServiceFulfillmentEngineWebJob
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				int userId = int.Parse(ConfigurationManager.AppSettings["FulfillmentUserId"]);
				string apiKey = ConfigurationManager.AppSettings["FulfillmentUserPrivateKey"];
				var manager = new FulfillmentManager(userId, apiKey);
				manager.FulfillNewRequests();

				Thread.Sleep(3000);		//so not a good idea. Should use a timer. 
			}
		}
	}
}
