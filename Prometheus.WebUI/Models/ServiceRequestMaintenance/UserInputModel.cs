using Common.Dto;
using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	/// <summary>
	/// Model used for views
	/// </summary>
	public class UserInputModel
	{
        /// <summary>
        /// What is current activity
        /// </summary>
	    public string Action { get; set; }
		public string OptionName { get; set; }
		/// <summary>
		/// Input type used for linking
		/// </summary>
		public UserInputType InputType { get; set; }
		/// <summary>
		/// User Input object to be used in the editor
		/// </summary>
		public IUserInput UserInput { get; set; }
        
	}
}