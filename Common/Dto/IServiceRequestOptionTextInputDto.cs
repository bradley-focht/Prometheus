using System;

namespace Common.Dto
{
	public interface IServiceRequestOptionTextInputDto : IServiceRequestOptionInputDto
	{
		int Id { get; set; }

		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int TextInputId { get; set; }
		int UpdatedByUserId { get; set; }

		ITextInputDto TextInput { get; set; }
	}
}