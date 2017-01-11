namespace Common.Dto
{
	public interface IScriptedSelectionDto : ISelectable
	{
		string Script { get; set; }
		bool ExecutionEnabled { get; set; }
	}
}
