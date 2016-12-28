using System;

namespace TestConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var context = new DataService.DataAccessLayer.PrometheusContext())
			{
				foreach (var option in context.ServiceOptions)
				{
					Console.WriteLine(option.Id);
				}

				Console.WriteLine("Donezo");
				Console.ReadLine();
			}
		}
	}
}
