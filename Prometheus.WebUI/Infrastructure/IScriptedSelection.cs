namespace Prometheus.WebUI.Infrastructure
{
	public interface IScriptedSelection : ISelectable
	{
		string Script { get; set; }
		bool ExecutionEnabled { get; set; }
	}
}
