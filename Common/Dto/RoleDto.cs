using System;
using System.Collections.Generic;

namespace ServicePortfolio.Dto
{
	public class RoleDto : IRoleDto
	{
		//PK
		public int Id { get; set; }

		//Fields
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual ICollection<IUserDto> Users { get; set; }
	}
}
