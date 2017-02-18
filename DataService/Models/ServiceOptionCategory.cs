using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceOptionCategory : IServiceOptionCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int ServiceId { get; set; }

		public int Popularity { get; set; }
		public string Features { get; set; }
		public string Benefits { get; set; }
		public string Support { get; set; }
		public string Description { get; set; }
	    public bool Quantifiable { get; set; }
	    public string BusinessValue { get; set; }
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual Service Service { get; set; }
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
		public virtual ICollection<ServiceRequestPackage> ServiceRequestPackages { get; set; }
	}
}
