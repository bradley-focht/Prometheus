using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	public class ServiceRequestOptionModel
	{
		public IServiceOptionDto Option { get; set; }
		public IEnumerable<IUserInput> UserInputs { get; set; }
	}
}