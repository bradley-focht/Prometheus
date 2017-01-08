using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Infrastructure
{
	public interface ISelectable : IUserInput
	{
		int NumberToSelect { get; set; }
	}
}