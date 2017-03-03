using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{

	/// <summary>
	/// Data to send to the controls partial view
	/// </summary>
	public class ServiceRequestApprovalControls
    {
		/// <summary>
		/// current page of total
		/// </summary>
        public int CurrentPage { get; set; }

		/// <summary>
		/// Total pages
		/// </summary>
		public int TotalPages { get; set; }
		/// <summary>
		/// Text describing applied filter(s)
		/// </summary>
		public string FilterText { get; set; }

		/// <summary>
		/// For pagination button
		/// </summary>
		public string FilterAction { get; set; }

		/// <summary>
		/// The state to filter
		/// </summary>
		public ServiceRequestState FilterState { get; set; }

		public int FilterRequestor { get; set; }
		/// <summary>
		/// Should filter state be applied for pagination
		/// </summary>
		public bool FilterStateRequired { get; set; }
		public bool FilterRequestorRequired { get; set; }
    }
}