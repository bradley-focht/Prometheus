using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceOptionModel
    {
		/// <summary>
		/// The option DTO
		/// </summary>
        public ServiceOptionDto Option { get; set; }
        public string ServiceName { get; set; }

		/// <summary>
		/// Contention is to use Update or Add
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// Next options for data validation
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		public string Name => Option.Name;

		[Required (ErrorMessage = "Valid number is required")]
		[Range(0, double.MaxValue, ErrorMessage = "Valid number is required")]
	    public double PriceUpFront => Option.PriceUpFront;

		[Required(ErrorMessage = "Valid number is required")]
		[Range(0, double.MaxValue, ErrorMessage = "Valid number is required")]
		public double PriceMonthly => Option.PriceMonthly;

		[Required(ErrorMessage = "Valid number is required")]
		[Range(0, double.MaxValue, ErrorMessage = "Valid number is required")]
		public double Cost => Option.Cost;

		/// <summary>
		/// Option categories for dropdown list
		/// </summary>
		public IEnumerable<SelectListItem> OptionCategories { get; set; }

		/// <summary>
		/// Used to display name rather than id
		/// </summary>
		public string CategoryName { get; set; }
	}
}