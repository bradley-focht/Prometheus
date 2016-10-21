using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class User : IUser
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int RoleId { get; set; }

		//Fields
		public string Name { get; set; }
		public string Password { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual Role Role { get; set; }
	}
}
