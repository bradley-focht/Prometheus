using System.Configuration;

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
			}
		}
	}
}
