using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// Item stored in a ServiceRequest
	/// </summary>
	public class ServiceRequestOption : IServiceRequestOption
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// ID of Service Option that SRO was created from
		/// </summary>
		public int ServiceOptionId { get; set; }

		/// <summary>
		/// ID of Service Request that SRO is a part of
		/// </summary>
		public int ServiceRequestId { get; set; }

		/// <summary>
		/// Number the the Service Option Requested
		/// </summary>
		public int Quantity { get; set; }

		/// <summary>
		/// If the request is Basic for the purposes of ApproveServiceRequest.ApproveBasicRequests
		/// </summary>
		public bool BasicRequest { get; set; }
		public string Code { get; set; }
		public string ServiceOptionName { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		public int RequestedByUserId { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		public int ApproverUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ServiceOption ServiceOption { get; set; }
		public virtual ServiceRequest ServiceRequest { get; set; }
	}
}
