using Common.Dto;


namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
	/// <summary>
	/// Orders data in a way that will make sense to a user
	/// </summary>
	public class DisplayListModelItem
	{
		/// <summary>
		/// User Data
		/// </summary>
		public IServiceRequestOptionDto ServiceRequestOption { get; set; }
		/// <summary>
		/// The SR Option from the service catalog
		/// </summary>
		public IServiceOptionDto ServiceOption { get; set; }
	}
}