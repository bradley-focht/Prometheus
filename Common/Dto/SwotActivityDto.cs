using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class SwotActivityDto : ISwotActivityDto
	{
		[HiddenInput]
		public int Id { get; set; }

		public int ServiceSwotId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// This is a title for the item
		/// </summary>
		[Required(ErrorMessage = "Name is reqired")]
		public string Name { get; set; }

		/// <summary>
		/// Optional extra text
		/// </summary>
		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// Date the activity took place on, or start date for multi-day events
		/// </summary>
		public DateTime? Date { get; set; }
		#endregion
	}
}
