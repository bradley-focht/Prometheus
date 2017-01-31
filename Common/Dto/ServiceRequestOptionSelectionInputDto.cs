using System;

namespace Common.Dto
{
	public class ServiceRequestOptionSelectionInputDto : IServiceRequestOptionSelectionInputDto
	{
		public int Id { get; set; }

		public int ServiceRequestOptionId { get; set; }
		public int SelectionInputId { get; set; }
		public string Value { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ISelectionInputDto SelectionInput { get; set; }
		public virtual IServiceRequestOptionDto ServiceRequestOption { get; set; }
	}
}