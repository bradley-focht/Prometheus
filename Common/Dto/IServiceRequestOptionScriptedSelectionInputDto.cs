using System;

namespace Common.Dto
{
	public interface IServiceRequestOptionScriptedSelectionInputDto : IServiceRequestOptionInputDto
	{
		int Id { get; set; }

		int ScriptedSelectionInputId { get; set; }

		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int UpdatedByUserId { get; set; }

		IScriptedSelectionInputDto ScriptedSelectionInput { get; set; }
	}
}