using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

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
        public ICollection<int> Associations { get; set; }

        /// <summary>
        /// services for selection
        /// </summary>
        public ICollection<IServiceDto> Services { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }
    }
}