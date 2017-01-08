namespace Prometheus.WebUI.Infrastructure
{
	public interface IUserInput
	{
		int Id { get; set; }
		string Name { get; set; }
		string HelpToolTip { get; set; }
	}
}