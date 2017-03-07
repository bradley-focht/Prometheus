using System.Collections.Generic;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class ManageUserDepartmentModel
	{
		public ICollection<UserDetailsModel> Users { get; set; }
	}
}