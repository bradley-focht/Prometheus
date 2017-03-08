using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class DepartmentModel
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name => SelectedDepartment.Name;
		public bool EnableAdd { get; set; }
		public DepartmentDto SelectedDepartment { get; set; }
		public IEnumerable<IDepartmentDto> Departments { get; set; }
	}
}