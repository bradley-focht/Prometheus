using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Shared
{
	public class ServiceRequestOption
	{
		public int OptionId { get; set; }
		public ICollection<KeyValuePair<string, string>> UserInputs { get; set; }
	}
}