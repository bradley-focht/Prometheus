using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public class ServiceRequestOptionDto : IServiceRequestOptionDto
	{
		public int Id { get; set; }

		public int ServiceOptionId { get; set; }
		public int ServiceRequestId { get; set; }
		public int Quantity { get; set; }

		public int RequestedByUserId { get; set; }
		public int ApproverUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual IServiceOptionDto ServiceOption { get; set; }
		public virtual IServiceRequestDto ServiceRequest { get; set; }
		public virtual ICollection<IServiceRequestOptionScriptedSelectionInputDto> ServiceRequestOptionScriptedSelectionInputs { get; set; }
		public virtual ICollection<IServiceRequestOptionSelectionInputDto> ServiceRequestOptionSelectionInputs { get; set; }
		public virtual ICollection<IServiceRequestOptionTextInputDto> ServiceRequestOptionTextInputs { get; set; }
	}
}
