using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
	public class ServiceSectionModel
	{
		public string Section { get; set; }
		public IServiceDto Service;

		/// <summary>
		/// Use either the Id for an int or Guid, whichever is relavent to the section item
		/// parentId if a second level of hierarchy
		/// </summary>
		public int SectionItemId { get; set; }
		public string ParentName { get; set; }
		public Guid SectionItemGuid { get; set; }
		public IEnumerable<SelectListItem> ServiceBundleNames { get; set; }
		public IEnumerable<SelectListItem> StatusNames { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name => Service.Name;
		[Required(ErrorMessage = "Lifecycle status is required")]
		public int LifecycStatusId => Service.LifecycleStatusId;
		[Required(ErrorMessage = "Service Bundle is required")]
		public int? ServiceBundleId => Service.ServiceBundleId;
	}
}