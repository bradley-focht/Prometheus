using System.Collections.Generic;

namespace Prometheus.WebUI.Infrastructure
{
	public interface ISelection : ISelectable
	{
		IEnumerable<string> SelectItems { get; set; }
	}
}