namespace Prometheus.WebUI.Infrastructure
{
	interface ITextInput : IUserInput
	{
		bool MultiLine { get; set; }
	}
}
