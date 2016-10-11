using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models
{
	public class LifecycleModel
	{
		public int CurrentStatus { get; set; }		//need to replace with lifecycle type
		public int Statuses { get; set; }
	}
}