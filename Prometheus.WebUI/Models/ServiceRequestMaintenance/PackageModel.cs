using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    /// <summary>
    /// This is an intermediary class to build service packages
    /// </summary>
    [Bind(Exclude = "Services, SelectedCategories")]
    
    public class PackageModel
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [HiddenInput]
        public int Id { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        
        /// <summary>
        /// collection of associated categories
        /// </summary>
        [Required(ErrorMessage = "At least one association is required")]
        public ICollection<string> Associations { get; set; }

		/// <summary>
		/// Action to which this package is relavent
		/// </summary>
		[Required (ErrorMessage = "Action is required")]
		public ServiceRequestAction Action { get; set; }
        /// <summary>
        /// services for selection
        /// </summary>
        public ICollection<IServiceDto> Services { get; set; }

		/// <summary>
		/// Existing selections
		/// </summary>
        public IEnumerable<int> SelectedCategories { get; set; }
		public IEnumerable<int> SelectedServices { get; set; }
	}
}