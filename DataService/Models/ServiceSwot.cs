using Common.Enums;
using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public class ServiceSwot : IServiceSwot
	{
		public int Id { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public string Item { get; set; }
		public ServiceSwotType Type { get; set; }

		public string Description { get; set; }
		#endregion
		#region Navigation Properties
		public ICollection<SwotActivity> SwotActivities { get; set; }
		#endregion
	}
}
