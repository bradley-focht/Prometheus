using System.ComponentModel.DataAnnotations;

namespace Prometheus.WebUI.Models.UserAccount
{
	/// <summary>
	/// Login credentials
	/// </summary>
    public class UserAccountModel
    {
		/// <summary>
		/// username
		/// </summary>
        [Required(ErrorMessage = "Enter username")]
        public string Username { get; set; }

		/// <summary>
		/// User password
		/// </summary>
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
		/// <summary>
		/// redirection after login if not entering from login
		/// </summary>
	    public string ReturnUrl { get; set; }

    }
}