using Common.Enums;
using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceRequest : IUserCreatedEntity
	{
		int Id { get; set; }

		int ServiceRequestPackageId { get; set; }

		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }

		string Comments { get; set; }
		string Officeuse { get; set; }
		DateTime CreationDate { get; set; }
		DateTime? SubmissionDate { get; set; }
		DateTime? ApprovalDate { get; set; }
		DateTime RequestedForDate { get; set; }

		ServiceRequestState State { get; set; }

		ServiceRequestPackage ServiceRequestPackage { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}