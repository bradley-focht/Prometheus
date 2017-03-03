using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class ServiceQueueModel
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name => SelectedQueue.Name;
		public bool EnableAdd { get; set; }
		public DepartmentDto SelectedQueue { get; set; }
		public IEnumerable<DepartmentDto> Queues { get; set; }
	}
}