
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class UpdateOptionCategoryModel
    {
        public OptionCategoryDto OptionCategory { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Action { get; set; }
		public IEnumerable<SelectListItem> Options { get; set; }

		[Required(ErrorMessage = "Name is required")]
	    public string Name => OptionCategory.Name;
    }
}