using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	public class RequestOptionFulfillment : ICreatedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int RequestFulfillmentId { get; set; }

		public int ServiceRequestOptionId { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual RequestFulfillment RequestFulfillment { get; set; }
	}
}