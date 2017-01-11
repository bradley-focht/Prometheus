using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Infrastructure;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	public class UserInputModel
	{
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