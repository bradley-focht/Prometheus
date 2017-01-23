using System.ComponentModel.DataAnnotations;

namespace Prometheus.WebUI.Models.UserAccount
{
    public class UserAccountModel
    {
        [Required(ErrorMessage = "Enter username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}