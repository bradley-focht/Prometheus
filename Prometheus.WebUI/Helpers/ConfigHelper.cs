using System.Configuration;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// reads settings from web.config
	/// </summary>
	public class ConfigHelper
	{
		/// <summary>
		/// get the marginally atractive rate of return
		/// </summary>
		/// <returns></returns>
		public static double GetMarr()
		{
			return double.Parse(ConfigurationManager.AppSettings["DefaultPwMarr"]);
		}

		/// <summary>
		/// get the period over which return is calculated
		/// </summary>
		/// <returns></returns>
		public static int GetPeriod()
		{
			return int.Parse(ConfigurationManager.AppSettings["DefaultPwPeriods"]);
		}

		/// <summary>
		/// get phone number (or other info) of single point of contact (service desk)
		/// </summary>
		/// <returns></returns>
		public static string GetSpoc()
		{
			return ConfigurationManager.AppSettings["Spoc"];
		}

		/// <summary>
		/// Default page size
		/// </summary>
		/// <returns></returns>
		public static int GetPaginationSize()
		{
			return int.Parse(ConfigurationManager.AppSettings["PaginationSize"]);
		}

		/// <summary>
		/// Default storage location for Service Option pictures
		/// </summary>
		/// <returns></returns>
		public static string GetOptionPictureLocation()
		{
			return ConfigurationManager.AppSettings["OptionPicsPath"];
		}

		/// <summary>
		/// Default delimiter for selection user inputs
		/// </summary>
		/// <returns></returns>
		public static string GetDelimiter()
		{
			return ConfigurationManager.AppSettings["Delimiter"];
		}

		/// <summary>
		/// Number of Service Catalog results to be assumed "the top"
		/// </summary>
		/// <returns></returns>
		public static string GetScTopAmount()
		{
			return ConfigurationManager.AppSettings["ScTopAmount"];
		}

		/// <summary>
		/// Current Domain
		/// </summary>
		/// <returns></returns>
		public static string GetDomain()
		{
			return ConfigurationManager.AppSettings["Domain"];
		}

		public static string GetServiceDocsPath()
		{
			return ConfigurationManager.AppSettings["ServiceDocsPath"];
		}

		/// <summary>
		/// Script files location
		/// </summary>
		/// <returns></returns>
		public static string GetScriptPath()
		{
			return ConfigurationManager.AppSettings["ScriptPath"];
		}

		/// <summary>
		/// Get the script set for identifying user department
		/// </summary>
		/// <returns></returns>
		public static int GetDepartmentScriptId()
		{
			return int.Parse(ConfigurationManager.AppSettings["GetDepartmentScriptId"]);
		}

		/// <summary>
		/// Get the script set to return all users in the same department
		/// </summary>
		/// <returns></returns>
		public static int GetDepartmentUsersScriptId()
		{
			return int.Parse(ConfigurationManager.AppSettings["GetDepartmentUsersScriptId"]);
		}

	}
}