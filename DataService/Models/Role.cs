using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class Role : IRole
	{
		//PK
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual ICollection<User> Users { get; set; }
	}
}
