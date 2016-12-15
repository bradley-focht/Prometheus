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
        /// <summary>
        /// Add new data if no data
        /// </summary>
        public string AddAction { get; set; }
        /// <summary>
        /// Section that is being rendered
        /// </summary>
        public string ServiceSection { get; set; }
        /// <summary>
        /// Id for actions and back links if any
        /// </summary>
        public int ServiceId { get; set; }
    }
}