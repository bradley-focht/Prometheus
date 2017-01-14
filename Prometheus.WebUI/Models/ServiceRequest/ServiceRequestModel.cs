using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using Ninject.Activation;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	public class ServiceRequestModel
	{
		public IServiceOptionDto Option { get; set; }

		public string Requestor { get; set; }
		public IEnumerable<string> Requestees { get; set; }
	}
}