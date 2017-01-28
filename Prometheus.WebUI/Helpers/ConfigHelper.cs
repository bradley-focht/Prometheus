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

        public static int GetPaginationSize()
        {
            return int.Parse(ConfigurationManager.AppSettings["PaginationSize"]);
        }

        public static string GetOptionPictureLocation()
        {
            return ConfigurationManager.AppSettings["OptionPicsPath"];
        }
    }
}