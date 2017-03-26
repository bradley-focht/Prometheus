
using System.Text;
using System.Text.RegularExpressions;

namespace Prometheus.WebUI.Helpers
{

	/// <summary>
	/// Helper functions for operations on text that don't fit into other functions
	/// </summary>
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
			if (input == null)
				return null; 

			return Regex.Replace(input, "([a-z])_?([A-Z])", "$1 $2");
		}

		/// <summary>
		/// Return a string with spaces removed
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string RemoveSpaces(string input)
		{
			return Regex.Replace(input, @"\s+", "");
		}
		/// <summary>
		/// Remove anything non alphaNumeric
		/// Good for html tag Ids
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string RemoveNonAlphaNum(string input)
		{
			return Regex.Replace(input, @"[^a-zA-Z0-9]", "");
		}

		/// <summary>
		/// Convert a delmited list to a javascript array
		/// </summary>
		/// <param name="input"></param>
		/// <param name="delimiter">only one char is allowed</param>
		/// <returns></returns>
		public static string ConvertStringtoJsValLabelArray(string input, char delimiter)
		{
			string[] words = input.Split(delimiter);
			StringBuilder builder = new StringBuilder();
		
			for(int i = 0; i<words.Length; i++)
			{
				builder.Append("{");
				builder.Append($"value:\"{words[i]}\", label: \"{words[i]}\"");
				builder.Append("}");
				if (i < (words.Length - 1))
					builder.Append(",");
			}

			return builder.ToString();
		}
	}
}