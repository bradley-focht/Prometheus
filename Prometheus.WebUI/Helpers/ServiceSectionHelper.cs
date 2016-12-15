using System.Collections.Generic;
using System.Linq;

namespace Prometheus.WebUI.Helpers
{
    /// <summary>
    /// Contains lists of strings for service sections to be used for conventions, which doesn't seem to be able to be used properly as an enum
    ///  RouteStrings - can be sent through the routing to decide which section or item should be shown
    ///  NavStrings - what is shown in the navigation bar on show/section
    ///  NavStringRoutes - the link that can be used in conjunction with the navString, this is a subset of routeStrings
    ///  FriendlyNavName - this is the individual item section titles for adding/updating an individual item
    /// </summary>
    public static class ServiceSectionHelper
    {
        public static List<string> RouteStrings => new List<string> {"General", "GeneralOnly", "Goals", "Swot", "SwotStrength", "SwotWeakness", "SwotOpportunity", "SwotThreat", "SwotActivities", "WorkUnits", "Contracts", "Measures", "Processes", "Pricing", "Documents"};
        public static List<string> NavStrings => new List<string> { "General", "Goals", "SWOT", "Work Units", "Contracts", "Measures", "Processes", "Pricing", "Documents" };
        public static List<string> NavStringRoutes => new List<string> { "General", "Goals", "Swot", "WorkUnits", "Contracts", "Measures", "Processes", "Pricing", "Documents" };
        public static List<string> FriendlyNavName => new List<string> {"General", "Goal", "SWOT Item", "Work Unit", "SWOT Activity", "Contract", "Measure", "Process", "Pricing Option", "Document"};

        public static List<KeyValuePair<string, string>> GenerateNavLinks()
        {
            //generate a list from NavStrings and NavStringRoutes
            return NavStringRoutes.Select((t, i) => new KeyValuePair<string, string>(NavStringRoutes.ElementAt(i), NavStrings.ElementAt(i))).ToList();
        }

        /// <summary>
        /// Intended purpose is to build back links
        /// </summary>
        /// <param name="navString"></param>
        /// <returns></returns>
        public static string ParentSection(string navString)
        {
            if (navString == "SwotActivity")            //this is currently the only hierarchical one
                return "Swot";

            return null;
        }

        public static bool ValidateRoute(string routeArg)
        {
            return RouteStrings.Any(s => s == routeArg);
        }

        /// <summary>
        /// Convert from the route-friendly string to the singular-item section name
        /// </summary>
        /// <param name="routeArg"></param>
        /// <returns></returns>
        public static string ConvertRouteStringToFriendlyName(string routeArg)
        {
            //special case is SWOT - which is a major pain to deal with. 
            if (routeArg.Contains("Acti"))
                return FriendlyNavName.Find(s => s.Contains("Acti"));

            if (routeArg.Contains("Swot"))
            {
                return FriendlyNavName.Find(s => s.Contains("SWOT I"));
            }

            //all other strings just match the first 3 letters and that should be sufficient
            routeArg = routeArg.Substring(0, 3);
            return FriendlyNavName.Find(s => s.Contains(routeArg));
        }

        /// <summary>
        /// returns the route-friendly equivalent of what is put in the navigation title bar
        /// </summary>
        /// <param name="navArg"></param>
        /// <param name="formPostNav">if conversion is being used in a form = true, otherwise routing for back-link navigation</param>
        /// <returns></returns>
        public static string ConvertNavStringToRouteString(string navArg, bool formPostNav = false)
        {
            if (navArg.Contains("Swot"))
            {
                return navArg.Contains("Activ") && formPostNav? "SwotActivity" : "Swot";
            }
            return navArg.Replace(" ", "");
        }

    }
}