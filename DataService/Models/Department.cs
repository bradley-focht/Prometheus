using System;

namespace DataService.Models
{
	public class Department: IDepartment
	{
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
