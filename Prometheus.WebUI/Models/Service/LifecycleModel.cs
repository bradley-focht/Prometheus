using System.Collections.Generic;
using DataService.Models;

namespace Prometheus.WebUI.Models.Service
{
	public class LifecycleModel
	{
		public ILifecycleStatus CurrentStatus { get; set; }		//need to replace with lifecycle type
		public IEnumerable<KeyValuePair<int, string>> Statuses { get; set; }
	}
}