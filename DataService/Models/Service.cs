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
		public Guid? Id { get; set; }
		//FK
		public Guid ServiceBundleId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid UpdatedByUserId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string BusinessOwner { get; set; }
		public string ServiceOwner { get; set; }
		public Guid LifecycleStatus { get; set; }
		public Guid ServiceTypeRole { get; set; }
		public Guid ServiceTypeProvision { get; set; }
		//public IEnumerable<ServiceRequest> ServiceRequests { get; set; }



		//Navigation Properties
		public virtual ServiceBundle ServiceBundle { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}
