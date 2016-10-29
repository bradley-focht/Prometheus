using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Models
{
	public class ServiceWorkUnit : IServiceWorkUnit
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		[Display(Name = "Work Unit")]
		public string WorkUnit { get; set; }

		public string Contact { get; set; }
		public string Responsibilities { get; set; }
	}
}
