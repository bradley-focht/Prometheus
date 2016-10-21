using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class Role : IRole
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//Fields
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual ICollection<User> Users { get; set; }
	}
}
