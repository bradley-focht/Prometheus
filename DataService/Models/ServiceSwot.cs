using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Entities;

namespace DataService.Models
{
	public class ServiceSwot : IServiceSwot
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }

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
		public virtual Service Service { get; set; }
		public virtual ICollection<SwotActivity> SwotActivities { get; set; }
		#endregion
	}
}
