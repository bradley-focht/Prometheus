using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	public class RequestFulfillment : ICreatedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int ServiceRequestId { get; set; }

		public ServiceRequestAction Action { get; set; }

		public string Name { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<RequestOptionFulfillment> RequestOptionFulfillments { get; set; }
	}
}
