using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestApprovalController : Controller
    {
        private IPortfolioService _ps;
        private readonly int _pageSize;

        public ServiceRequestApprovalController()
        {
            try
            {
                _pageSize = ConfigHelper.GetPaginationSize();
            }
            catch (Exception) { _pageSize = 12; }       //just in case
        }
        
        /// <summary>
        /// display the home/portal page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string filterBy, string filterArgs, int pageId = 0)
        {
            int userId;     //user info
            try { userId = int.Parse(Session["Id"].ToString()); }                   //login is required
            catch (Exception) { return RedirectToAction("Index", "UserAccount");  }  //some login issue

            ServiceRequestApprovalModel model = new ServiceRequestApprovalModel {Controls = new ServiceRequestApprovalControls {CurrentPage = pageId} };

            _ps = InterfaceFactory.CreatePortfolioService(userId);
            List<ServiceRequestTableItemModel> requests = new List<ServiceRequestTableItemModel>();

            var srList = _ps.GetServiceRequestsForRequestorId(userId).ToList();      //for pagination
            if (srList.Count > _pageSize)
            {
                model.Controls.TotalPages = (srList.Count + _pageSize - 1) / _pageSize;
                srList = srList.Skip(_pageSize * pageId).Take(_pageSize).ToList();
            }

            foreach (var item in srList)
            {
                if (item.ServiceOptionId != null)       //if null than this isn't an SR that can be reconstructed in a form
                {
                    requests.Add(new ServiceRequestTableItemModel
                    {
                        Id = item.Id,
                        State = item.State,
                        PackageName = ServicePackageHelper.GetPackage(_ps, item.ServiceOptionId).Name,
                        DateRequired = item.RequestedForDate,
                        DateSubmitted = item.SubmissionDate
                    });
                }
            }
            model.ServiceRequests = requests;
            return View(model);
        }
    }
}