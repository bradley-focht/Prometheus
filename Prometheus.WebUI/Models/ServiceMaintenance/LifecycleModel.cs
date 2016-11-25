using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceMaintenance
{
	public class LifecycleModel
	{
		public ILifecycleStatusDto CurrentStatus { get; set; }		//need to replace with lifecycle type
	    public int Count { get; set; }
	}
}