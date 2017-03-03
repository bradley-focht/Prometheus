using System;
using System.Web.Mvc;

namespace Prometheus.WebUI.Infrastructure
{
	/// <summary>
	/// Implement session
	/// </summary>
	public abstract class PrometheusController : Controller
	{
		/// <summary>
		/// Return current user id from session
		/// </summary>
		protected int UserId
		{
			get
			{
				try
				{
					return int.Parse(Session["Id"].ToString());
				}
				catch (Exception)
				{
					return 0;
				}
			}
		}
	}
}