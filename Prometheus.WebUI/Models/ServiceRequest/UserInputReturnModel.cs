using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// Group a User Input with the ServiceOptions it is associated with
	/// </summary>
	public class UserInputReturnModel
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public UserInputTypes ControlType { get; set; }
		public int InputId { get; set; }

	}
}