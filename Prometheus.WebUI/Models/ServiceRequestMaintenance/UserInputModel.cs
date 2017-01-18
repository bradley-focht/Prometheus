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
		/// used for back links
		/// </summary>
		public string ServiceName { get; set; }
		/// <summary>
		/// used for back links
		/// </summary>
		public int ServiceId { get; set; }
		/// <summary>
		/// used for display
		/// </summary>
		public string Action { get; set; }
		/// <summary>
		/// Option Id for back links
		/// </summary>
		public int OptionId { get; set; }
		/// <summary>
		/// Option name for back links
		/// </summary>
		public string OptionName { get; set; }
		/// <summary>
		/// Input type used for linking
		/// </summary>
		public UserInputTypes InputType { get; set; }
		/// <summary>
		/// User Input object to be used in the editor
		/// </summary>
		public IUserInput UserInput { get; set; }
	}
}