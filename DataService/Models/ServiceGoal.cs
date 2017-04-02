using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Entities;

namespace DataService.Models
{
	public class ServiceGoal : IServiceGoal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// ID of Service that the Goal applies to
		/// </summary>
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique descriptive name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Extra text for those who like to talk
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Identify if short term or long term
		/// </summary>
		public ServiceGoalType Type { get; set; }

		/// <summary>
		/// Date that the Goal begins
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Date that the Goal ends
		/// </summary>
		public DateTime? EndDate { get; set; }
		#endregion
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
