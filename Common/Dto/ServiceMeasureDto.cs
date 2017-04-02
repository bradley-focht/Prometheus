using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceMeasureDto : IServiceMeasureDto
	{
		//TODO: Brad comment
		public int Id { get; set; }

		/// <summary>
		/// ID of the Service that the Measure applies to 
		/// </summary>
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Measurement method used, such as survey
		/// </summary>
		[Required(ErrorMessage = "Method name is required")]
		public string Method { get; set; }

		/// <summary>
		/// Results of the measurement method used
		/// </summary>
		[AllowHtml]
		public string Outcome { get; set; }
		#endregion
	}
}
