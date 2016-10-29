using Common.Enums;
using System;

namespace DataService.Models
{
	public class ServiceGoal : IServiceGoal
	{
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
	}
}
