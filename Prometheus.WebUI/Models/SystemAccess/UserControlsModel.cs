using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.SystemAccess
{
    /// <summary>
    /// User Controls partial view model
    /// </summary>
    public class UserControlsModel
    {
        /// <summary>
        /// Roles to show in the filter list
        /// </summary>
        public IEnumerable<Tuple<int, string>> Roles { get; set; }
        /// <summary>
        /// Display text of applied filter
        /// </summary>
        public string AppliedFilter { get; set; }
        /// <summary>
        /// number of page buttons to display
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Currently active page button
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Routing argument
        /// </summary>
        public int SelectedRole { get; set; }
    }
}