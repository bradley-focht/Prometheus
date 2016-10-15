using System.Collections.Generic;
using DataService.Models;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleEditorModel : ServiceBundleModel
	{
		public IEnumerable<KeyValuePair<int, string>> Services { get; set; }

	    public ServiceBundleEditorModel(IServiceBundle currentServiceBundle) : base(currentServiceBundle)
	    {
	    }
	}
}