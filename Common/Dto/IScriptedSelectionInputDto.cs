namespace Common.Dto
{
	/// <summary>
	/// Combobox selection style input where selection options are generated via a script
	/// </summary>
	public interface IScriptedSelectionInputDto : ISelectable
	{
		/// <summary>
		/// Script to execute
		/// </summary>
		int ScriptId { get; set; }

		/// <summary>
		/// Allow the execution of the script
		/// </summary>
		bool ExecutionEnabled { get; set; }
	}
}
