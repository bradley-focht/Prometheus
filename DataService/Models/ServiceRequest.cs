using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceRequest : IServiceRequest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int RequestedByUserId { get; set; }
	    public string Comments { get; set; }    /*fields added by brad */
	    public string Officeuse { get; set; }
	    public DateTime CreationDate { get; set; }
	    public DateTime? SubmissionDate { get; set; }
	    public DateTime? ApprovalDate { get; set; } /* end here */
	    public int ApproverUserId { get; set; }
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}
