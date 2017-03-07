namespace Common.Dto
{
	public interface IScriptedSelectionInputDto : ISelectable
	{
		int ScriptId { get; set; }
		bool ExecutionEnabled { get; set; }
	}
}
