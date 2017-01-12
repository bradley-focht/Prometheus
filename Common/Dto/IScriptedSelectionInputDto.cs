namespace Common.Dto
{
	public interface IScriptedSelectionInputDto : ISelectable
	{
		string Script { get; set; }
		bool ExecutionEnabled { get; set; }
	}
}
