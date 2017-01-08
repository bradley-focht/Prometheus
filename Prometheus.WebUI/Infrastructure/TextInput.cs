namespace Prometheus.WebUI.Infrastructure
{
	public class TextInput : ITextInput
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string HelpToolTip { get; set; }
		public bool MultiLine { get; set; }
	}
}