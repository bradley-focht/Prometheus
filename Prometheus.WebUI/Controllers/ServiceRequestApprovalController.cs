using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequest;
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

		/// <summary>
		/// Generic state change screen, will adjust to the state
		/// </summary>
		/// <param name="id">Service Requeset Id</param>
		/// <param name="nextState">next SR state to change to</param>
		/// <returns></returns>
		public ActionResult ConfirmServiceRequestStateChange(int id, ServiceRequestState nextState)
		{
			int userId;
			try { userId = int.Parse(Session["Id"].ToString()); }
			catch(Exception) { return null; }

			ServiceRequestStateChangeModel model = new ServiceRequestStateChangeModel {NextState = nextState};

			_ps = InterfaceFactory.CreatePortfolioService(userId);
			model.ServiceRequestModel = new ServiceRequestModel();
			model.ServiceRequestModel.ServiceRequest = _ps.GetServiceRequest(id);
			model.ServiceRequestModel.Package = ServicePackageHelper.GetPackage(_ps, model.ServiceRequestModel.ServiceOptionId);

			List<DisplayListModel> displayList = new List<DisplayListModel>();

			foreach (var category in model.ServiceRequestModel.Package.ServiceOptionCategoryTags)
			{
				DisplayListModel listItem = new DisplayListModel {Category = category.ServiceOptionCategory, Options = new List<DisplayListModelItem>()};
				
				foreach (var option in _ps.GetServiceOptionCategory(category.ServiceOptionCategoryId).ServiceOptions)
				{
					foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)
					{
						if (serviceRequestOption.ServiceOptionId == option.Id)
						{
							listItem.Options.Add(new DisplayListModelItem {ServiceOption = option, ServiceRequestOption = serviceRequestOption});
							
						}
						
					}
				}
				displayList.Add(listItem);
			}
			model.DisplayList = displayList;
			return View(model);
		}

	}
}