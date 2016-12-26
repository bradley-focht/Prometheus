using Common.Enums;
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

		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ServiceGoalType Type { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		#endregion
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
