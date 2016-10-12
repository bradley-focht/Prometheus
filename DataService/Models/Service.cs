using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class Service : IService
	{
		//PK
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Id { get; set; }
		//FK
		public Guid ServiceBundleId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid UpdatedByUserId { get; set; }

		//Navigation Properties
		public virtual ServiceBundle ServiceBundle { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}
