using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class UserModel
	{
		public ICollection<Tuple<Guid, string>> Users { get; set; }

		public ICollection<RoleDto> Roles { get; set; }
		
		public bool ReturningSearch { get; set; }
	}
}