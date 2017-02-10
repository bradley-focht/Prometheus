using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequestApproval;
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
            int userId;     //user info
            try { userId = int.Parse(Session["Id"].ToString()); }                   //login is required
            catch (Exception) { return RedirectToAction("Index", "UserAccount");  }  //some login issue

            ServiceRequestApprovalModel model = new ServiceRequestApprovalModel {Controls = new ServiceRequestApprovalControls()};

            _ps = InterfaceFactory.CreatePortfolioService(userId);
            List<ServiceRequestTableItemModel> requests = new List<ServiceRequestTableItemModel>();
            foreach(var item in _ps.GetServiceRequestsForRequestorId(userId))
            {
                if (item.ServiceOptionId != null)       //if null than this isn't an SR that can be reconstructed in a form
                {
                    requests.Add(new ServiceRequestTableItemModel
                    {
                        Id = item.Id,
                        State = item.State,
                        ServiceOptionId = item.ServiceOptionId.Value,
                        PackageName = ServicePackageHelper.GetPackage(_ps, item.ServiceOptionId).Name
                    });
                }
            }
            model.ServiceRequests = requests;
            return View(model);
        }
    }
}