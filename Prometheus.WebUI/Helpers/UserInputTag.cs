using Common.Dto;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// Used for a "venn diagram" for user inputs
	/// </summary>
	public class UserInputTag
	{
		public ServiceRequestUserInputDto UserInput { get; set; }
		public bool Required { get; set; }
	}
}