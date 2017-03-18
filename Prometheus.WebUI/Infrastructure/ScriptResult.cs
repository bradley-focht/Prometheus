namespace Prometheus.WebUI.Infrastructure
{
	public class ScriptResult<TValue, TLabel>
	{
		public TValue Value { get; set; }
		public TLabel Label { get; set; }
	}
}
