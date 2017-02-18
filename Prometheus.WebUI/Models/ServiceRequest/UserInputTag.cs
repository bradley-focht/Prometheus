using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// Group a User Input with the ServiceOptions it is associated with
	/// </summary>
	public class UserInputTag
	{
		public IUserInput UserInput { get; set; }
		public IEnumerable<int> ServiceOptionIds { get; set; }
	}
}