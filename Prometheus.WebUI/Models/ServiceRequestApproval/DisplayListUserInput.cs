using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using DataService.Models;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
	public class DisplayListUserInput
	{
		public string DisplayName { get; set; }
		public IServiceRequestUserInputDto ServiceRequestUserInput { get; set; }
	}
}