using System;

namespace Common.Dto
{
	public class ServiceRequestOptionDto : IServiceRequestOptionDto
	{
		public int Id { get; set; }

		public int ServiceOptionId { get; set; }
		public int ServiceRequestId { get; set; }
		public int Quantity { get; set; }
		public string Code { get; set; }
		public string ServiceOptionName { get; set; }
		public bool BasicRequest { get; set; }


		public int RequestedByUserId { get; set; }
		public int ApproverUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual IServiceOptionDto ServiceOption { get; set; }
		public virtual IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ServiceRequest { get; set; }
	}
}
