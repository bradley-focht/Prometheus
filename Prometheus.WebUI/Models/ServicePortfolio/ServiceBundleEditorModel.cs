using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleEditorModel : ServiceBundleModel
	{
		public IEnumerable<KeyValuePair<int, string>> Services { get; set; }

	    public ServiceBundleEditorModel(ServiceBundleDto currentServiceBundle) : base(currentServiceBundle)
	    {
	    }
	}
}