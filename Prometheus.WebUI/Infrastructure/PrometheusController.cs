using System;
using System.Web.Mvc;

namespace Prometheus.WebUI.Infrastructure
{
	public abstract class PrometheusController : Controller
	{
		/// <summary>
		/// Return current user id from session
		/// </summary>
		protected int Userid
		{
			get
			{
				int userId = 0;
				try
				{
					return userId = int.Parse(Session["Id"].ToString());
				}
				catch (Exception)
				{
					return 0;
				}
			}
		}
	}
}