
using System.Text.RegularExpressions;

namespace Prometheus.WebUI.Helpers
{
	public static class StringHelper
	{
		/*
		 *  This code is a regex that comes from Guffa posted @ http://stackoverflow.com/questions/14946933/add-spacing-between-lowercase-and-uppercase
		 * */
		/// <summary>
		/// Convert something in camel case to a properly spaced string
		///  maintains case
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string CamelToString(string input)
		{
			return Regex.Replace(input, "([a-z])_?([A-Z])", "$1 $2");
		}
	}
}