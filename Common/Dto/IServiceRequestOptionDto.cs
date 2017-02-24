using System;

namespace Common.Dto
{
	/// <summary>
	/// Items stored in a ServiceRequest
	/// </summary>
	public interface IServiceRequestOptionDto
	{
		int Id { get; set; }

		int ServiceOptionId { get; set; }
		int ServiceRequestId { get; set; }
		int Quantity { get; set; }
		int ApproverUserId { get; set; }
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int RequestedByUserId { get; set; }
		int UpdatedByUserId { get; set; }

		IServiceOptionDto ServiceOption { get; set; }
		IServiceRequestDto ServiceRequest { get; set; }
	}
}