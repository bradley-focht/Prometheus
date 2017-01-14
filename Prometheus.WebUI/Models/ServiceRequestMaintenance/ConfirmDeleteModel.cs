using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	public class ConfirmDeleteModel
	{
		public string OptionName { get; set; }
		public int OptionId { get; set; }
		public string Name { get; set; }
		public string DeleteAction { get; set; }
		public string ReturnAction { get; set; }
		public UserInputTypes Type { get; set; }
		public int Id { get; set; }
	}
}