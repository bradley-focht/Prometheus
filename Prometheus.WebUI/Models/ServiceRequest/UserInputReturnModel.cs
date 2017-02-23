using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// Group a User Input with the ServiceOptions it is associated with
	/// </summary>
	public class UserInputReturnModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public UserInputType Type { get; set; }
		public int InputId { get; set; }

	}
}