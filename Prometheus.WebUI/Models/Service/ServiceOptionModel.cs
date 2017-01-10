using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceOptionModel
    {
		/// <summary>
		/// The option DTO
		/// </summary>
        public ServiceOptionDto Option { get; set; }
        public string ServiceName { get; set; }

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