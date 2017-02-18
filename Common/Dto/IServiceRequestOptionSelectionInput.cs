using System;

namespace Common.Dto
{
	public interface IServiceRequestOptionSelectionInputDto : IServiceRequestOptionInputDto
	{
		int Id { get; set; }

		int SelectionInputId { get; set; }

		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int UpdatedByUserId { get; set; }

		ISelectionInputDto SelectionInput { get; set; }
	}
}