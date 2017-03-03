using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class ManageUserDepartmentModel
	{
		public ICollection<UserDetailsModel> Users { get; set; }
	}
}