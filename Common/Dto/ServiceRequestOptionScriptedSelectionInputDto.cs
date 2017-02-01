using System;

namespace Common.Dto
{
	public class ServiceRequestOptionScriptedSelectionInputDto : IServiceRequestOptionScriptedSelectionInputDto
	{
		public int Id { get; set; }

		public int ServiceRequestOptionId { get; set; }
		public int ScriptedSelectionInputId { get; set; }
		public string Value { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual IScriptedSelectionInputDto ScriptedSelectionInput { get; set; }
		public virtual IServiceRequestOptionDto ServiceRequestOption { get; set; }
	}
}
