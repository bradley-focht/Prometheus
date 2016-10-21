using System;

namespace DataService.Models
{
	//TODO Brad tell Sean how this relates to the other entities
	public class ServiceGoals : IServiceGoals
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
