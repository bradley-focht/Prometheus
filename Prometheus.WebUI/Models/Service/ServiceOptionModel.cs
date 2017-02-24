using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
	public class ServiceOptionModel
	{
		/// <summary>
		/// The option DTO
		/// </summary>
		public IServiceOptionDto Option { get; set; }

		/// <summary>
		/// For back links
		/// </summary>
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }

		/// <summary>
		/// Contention is to use Update or Add
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// Used to display name rather than id
		/// </summary>
		public string CategoryName { get; set; }
	}
}