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

		/// <summary>
		/// Check permissions
		/// </summary>
		/// <param name="id">permission id</param>
		/// <returns></returns>
		public ActionResult HasBusinessCatalogPermision(BusinessCatalog id)
		{
					if (_userManager.UserHasPermission(UserId, id))
						return new HttpStatusCodeResult(HttpStatusCode.OK);

			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasSupportCatalogPermission(SupportCatalog id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasMyRequestsPermission(ServiceRequestSubmission id )
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasServiceDetailsPermission(ServiceDetails id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasServicePortfolioPermission(ServicePortfolio id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasRequestMaintenancePermission(ServiceCatalogMaintenance id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasRoleAdjustmentPermission(RolePermissionAdjustment id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasServiceMaintenancePermission(ServiceDetails id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}

		public ActionResult HasScriptMaintenancePermission(ScriptAccess id)
		{
			{
				if (_userManager.UserHasPermission(UserId, id))
					return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}
	}
}