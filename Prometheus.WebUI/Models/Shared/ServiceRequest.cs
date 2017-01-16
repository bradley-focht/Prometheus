using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models.Shared
{
	public class ServiceRequest
	{
		public string Requestor { get; set; }
		public List<UserInput> UserInputs { get; set; }

	}
}