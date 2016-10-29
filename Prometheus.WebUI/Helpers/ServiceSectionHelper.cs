using System.Collections.Generic;
using System.Linq;

namespace Prometheus.WebUI.Helpers
{
    //contains lists of strings for service sections to be used for conventions, which doesn't seem to be able to be used properly as an enum
    public static class ServiceSectionHelper
    {
        public static List<string> RouteStrings => new List<string> {"General", "GeneralOnly", "Goals", "Swot", "SwotActivities", "WorkUnits", "Contracts", "Measures", "Processes", "Pricing", "Documents"};
        public static List<string> NavStrings => new List<string> { "General", "Goals", "SWOT", "Work Units", "Contracts", "Measures", "Processes", "Pricing", "Documents" };
        public static List<string> NavStringRoutes => new List<string> { "General", "Goals", "Swot", "WorkUnits", "Contracts", "Measures", "Processes", "Pricing", "Documents" };
        public static List<string> FriendlyNavName => new List<string> {"General", "Goal", "SWOT Item", "Work Unit", "SWOT Activity", "Contract", "Measure", "Process", "Pricing Option", "Document"};

        public static List<KeyValuePair<string, string>> GenerateNavLinks()
        {
            //generate a list from NavStrings and NavStringRoutes
            return NavStringRoutes.Select((t, i) => new KeyValuePair<string, string>(NavStringRoutes.ElementAt(i), NavStrings.ElementAt(i))).ToList();
        }

        public static bool ValidateRoute(string routeArg)
        {
            return RouteStrings.Any(s => s == routeArg);
        }

        public static string ConvertRouteStringToFriendlyName(string routeArg)
        {
            //special case is SWOT
            if (routeArg.Contains("Acti"))
                return FriendlyNavName.Find(s => s.Contains("Acti"));
            else if (routeArg.Contains("Swot"))
            {
                return FriendlyNavName.Find(s => s.Contains("SWOT I"));
            }

            //all other strings just match the first 3 letters and that should be sufficient
            routeArg = routeArg.Substring(0, 3);
            return FriendlyNavName.Find(s => s.Contains(routeArg));
        }

    }
}