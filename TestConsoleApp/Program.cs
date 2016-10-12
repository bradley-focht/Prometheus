using System;

namespace TestConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var context = new DataService.DataAccessLayer.PrometheusContext())
			{
				foreach (var option in context.ServiceRequestOptions)
				{
					Console.WriteLine(option.Id);
				}
			}
		}
	}
}
