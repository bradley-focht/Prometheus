using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class RoleModel
	{
		public string Action { get; set; }
		public RoleDto Role { get; set; }
		public int Id => Role.Id;
	}
}