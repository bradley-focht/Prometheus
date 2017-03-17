using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	/// <summary>
	/// Reduced set of attributes of an option for Service Request Maintenance tasks
	/// </summary>
    public class ServiceOptionAbbreviatedModel : ICatalogPublishable
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceOptionCategoryId { get; set; }
        public int Popularity { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string BusinessValue { get; set; }

		public bool Published { get; set; }
		public Guid? Picture { get; set; }
        public string PictureMimeType { get; set; }
        [AllowHtml]
        public string Details { get; set; }
		public bool BasicRequest { get; set; }

	}
}