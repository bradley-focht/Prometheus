using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Shared
{
    public class TableDataModel
    {
        public ICollection<string> Titles { get; set; }
        public ICollection<Tuple<int, ICollection<string>>> Data { get; set; }
        public string Controller { get; set; }
        /// <summary>
        /// Default action when item is clicked
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Confirm Deletion
        /// </summary>
        public string ConfirmDeleteAction { get; set; }
        /// <summary>
        /// Update Item
        /// </summary>
        public string UpdateAction { get; set; }

        public string ServiceSection { get; set; }
    }
}