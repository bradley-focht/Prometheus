using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class ManageUsersModel
	{
		public ICollection<UserDetailsModel> Users { get; set; }
		public ICollection<RoleDto> Roles { get; set; }
		public bool ReturningSearch { get; set; }
        public UserControlsModel Controls { get; set; }
	}
}