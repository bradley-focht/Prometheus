using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// Used for the ServiceOption partial view
	/// </summary>
	public class ServiceOptionModel
	{
		public IServiceOptionDto ServiceOption { get; set; }
		public bool Quantifiable { get; set; }
	}
}