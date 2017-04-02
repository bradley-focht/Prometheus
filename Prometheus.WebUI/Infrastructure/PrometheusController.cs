using System;
using System.Net;
using System.Web.Mvc;
using Common.Enums.Permissions;
using UserManager;

namespace Prometheus.WebUI.Infrastructure
{
	/// <summary>
	/// Implement session
	/// </summary>
	public abstract class PrometheusController : Controller
	{
		protected IUserManager _userManager;
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