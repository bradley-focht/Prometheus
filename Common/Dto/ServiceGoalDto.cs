using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums.Entities;


namespace Common.Dto
{

	public class ServiceGoalDto : IServiceGoalDto
	{
		[HiddenInput]
		public int Id { get; set; }
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique descriptive name
		/// </summary>
		[Display(Order = 1)]
        [Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		/// <summary>
		/// Extra text for those who like to talk
		/// </summary>
		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// Identify if short term or long term
		/// </summary>
		public ServiceGoalType Type { get; set; }

        [Display(Name = "Start Date")]
		public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
		#endregion
	}
}
