using System.Collections.Generic;
using Common.Dto;


namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
	/// <summary>
	/// Orders data in a way that will make sense to a user
	/// </summary>
	public class DisplayListOption
	{
		/// <summary>
		/// User Data
		/// </summary>
		public IServiceRequestOptionDto ServiceRequestOption { get; set; }
		/// <summary>
		/// The SR Option from the service catalog
		/// </summary>
		public IServiceOptionDto ServiceOption { get; set; }

		/// <summary>
		/// User input data
		/// </summary>
		public List<DisplayListUserInput> UserInputs { get; set; }

	}
}