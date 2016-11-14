using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceMaintenance
{
	public class LifecycleModel
	{
		public ILifecycleStatusDto CurrentStatus { get; set; }		//need to replace with lifecycle type
		public IEnumerable<Tuple<int, string>> Statuses { get; set; }
	}
}