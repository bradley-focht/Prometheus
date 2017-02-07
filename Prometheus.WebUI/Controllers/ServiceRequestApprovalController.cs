using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prometheus.WebUI.Helpers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestApprovalController : Controller
    {
        private IPortfolioService _ps;

        
        /// <summary>
        /// display the home/portal page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int userId;
            
            try { userId = int.Parse(Session["Id"].ToString()); }   //login is required
            catch (Exception) { return RedirectToAction("Index", "UserAccount");    //some login issue
            }

            _ps = InterfaceFactory.CreatePortfolioService(userId);
            var requests = _ps.GetServiceRequestsForRequestorId(userId);

            return View(requests);
        }
    }
}