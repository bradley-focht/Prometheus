using Prometheus.WebUI.Infrastructure;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	public class AddUserInputModel
	{
		public int OptionId { get; set; }
		public string OptionName { get; set; }
		public UserInputTypes InputType { get; set; }
	}
}