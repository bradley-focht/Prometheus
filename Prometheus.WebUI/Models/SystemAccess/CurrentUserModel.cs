using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class CurrentUserModel
	{
		public ICollection<UserDto> Users { get; set; }

		public ICollection<RoleDto> Roles { get; set; }
		public int SelectedUser { get; set; }
	}
}