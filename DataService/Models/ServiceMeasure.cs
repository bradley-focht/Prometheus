using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceMeasure : IServiceMeasure
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
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
		public string Method { get; set; }

		/// <summary>
		/// Results of the measurement method used
		/// </summary>
		public string Outcome { get; set; }
		#endregion
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
