using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceWorkUnitDto : IServiceWorkUnitDto
	{
		[HiddenInput]
		public int Id { get; set; }
		[HiddenInput]
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// The title of a team in the company
		/// </summary>
		[Display(Name = "Work Unit")]
        [Required(ErrorMessage = "Work Unit name is required")]
		public string WorkUnit { get; set; }

		/// <summary>
		/// A manager or someone's name to contact
		/// </summary>
		public string Contact { get; set; }

		/// <summary>
		/// A list of things that this team does to support this service
		/// </summary>
		[AllowHtml]
		public string Responsibilities { get; set; }
		#endregion
	}
}
