using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	public class ServiceOptionTag
	{
		public IServiceOptionDto ServiceOption { get; set; }
		public IInputGroupDto UserInputs { get; set; }
	}
}