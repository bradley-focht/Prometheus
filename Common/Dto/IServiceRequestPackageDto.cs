using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public interface IServiceRequestPackageDto
	{
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique Id
		/// </summary>
		[HiddenInput]
		int Id { get; set; }

		/// <summary>
		/// Unique name
		/// </summary>
		[Display(Order = 1)]
		string Name { get; set; }
		ICollection<IServiceOptionCategoryDto> ServiceOptionCategories { get; set; }

	}
}