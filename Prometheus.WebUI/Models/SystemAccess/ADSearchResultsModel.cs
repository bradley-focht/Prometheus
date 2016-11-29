using System;
using System.Collections.Generic;


namespace Prometheus.WebUI.Models.SystemAccess
{
    public class AdSearchResultsModel
    {
        public ICollection<Tuple<Guid, string>> SearchResults { get; set; }

        //roles
    }
}