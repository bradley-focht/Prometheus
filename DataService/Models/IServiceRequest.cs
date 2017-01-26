using Common.Enums;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceRequest : IUserCreatedEntity
	{
		int Id { get; set; }
		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }
		ServiceRequestState State { get; set; }

		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}