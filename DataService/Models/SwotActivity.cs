using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class SwotActivity : ISwotActivity
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceSwotId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// This is a title for the item
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Optional extra text
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Date the activity took place on, or start date for multi-day events
		/// </summary>
		public DateTime Date { get; set; }
		#endregion
		#region Navigation Propeties
		public virtual ServiceSwot ServiceSwot { get; set; }
		#endregion
	}
}
