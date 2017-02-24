using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	/// <summary>
	/// Used for the Manage Users view
	/// </summary>
	public class ManageUsersModel
	{
		/// <summary>
		/// List of users to display
		/// </summary>
		[Required(ErrorMessage = "Select one or more users")]
		public ICollection<UserDetailsModel> Users { get; set; }
		/// <summary>
		/// List of roles to display
		/// </summary>
		[Required(ErrorMessage = "Select one or more roles")]
		public ICollection<IRoleDto> Roles { get; set; }
		/// <summary>
		/// Show message if no results were found
		/// </summary>
		public bool ReturningSearch { get; set; }
		/// <summary>
		/// User input controls for the viwe
		/// </summary>
		public UserControlsModel Controls { get; set; }
	}
}