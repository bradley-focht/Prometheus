using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
	public class DisplayListModel
	{
		public IServiceOptionCategoryDto Category { get; set; }

		public List<DisplayListModelItem> Options { get; set; }
	}
}