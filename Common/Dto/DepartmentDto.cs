using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// Service Request Queue Associated with users
	/// </summary>
	public class DepartmentDto : IDepartmentDto
	{
		/// <summary>
		/// Unique Id
		/// </summary>
		[HiddenInput]
		public int Id { get; set; }
		/// <summary>
		/// Queue name to match script result
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public ICollection<IUserDto> Users { get; set; }
	}
}
