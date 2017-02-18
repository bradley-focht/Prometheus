using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    /// <summary>
    /// For the UserInputsLinkList Partial View]
    /// </summary>
    public class UserInputsLinkListModel
    {
        /// <summary>
        /// The data list
        /// </summary>
        public IEnumerable<Tuple<UserInputTypes, int, string>> Items { get; set; }
        /// <summary>
        /// For identification of which input to highlight
        /// </summary>
        public int SelectedInputId { get; set; }
        public UserInputTypes SelectedInputType { get; set; }
        /// <summary>
        /// actions 
        /// </summary>
        public string AddAction { get; set; }
        public string Action { get; set; }
    }
}