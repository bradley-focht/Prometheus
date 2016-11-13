using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Helpers
{
    public class ServiceDocumentHelper
    {
        /// <summary>
        /// Returns the file path that is configured in the Web.config file. 
        ///   this method looks for the AppSettings Filepath to be added
        /// </summary>
        /// <returns></returns>
        public static string FilePath => ConfigurationManager.AppSettings["FilePath"];
    }
}