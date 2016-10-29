using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
	public class LifecycleModel
	{
		public ILifecycleStatusDto CurrentStatus { get; set; }		//need to replace with lifecycle type
		public IEnumerable<KeyValuePair<int, string>> Statuses { get; set; }
	}
}