using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	public class RoleModel
	{
		public string Action { get; set; }
		public IRoleDto Role { get; set; }
		public int Id => Role.Id;
	}
}