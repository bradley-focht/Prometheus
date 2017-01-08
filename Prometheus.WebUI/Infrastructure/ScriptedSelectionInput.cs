namespace Prometheus.WebUI.Infrastructure
{
	public class ScriptedSelectionInput : IScriptedSelection
	{
		public string Script { get; set; }
		public bool ExecutionEnabled { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string HelpToolTip { get; set; }
		public int NumberToSelect { get; set; }
	}
}