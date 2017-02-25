using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Enums;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using RequestService;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceRequestApprovalController : Controller
	{
		private IPortfolioService _ps;
		private readonly int _pageSize;
		private IRequestManager _rm;

		public ServiceRequestApprovalController()
		{
			_rm = InterfaceFactory.CreateRequestManager();
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
		public ActionResult Index(int pageId = 0)
		{
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel { Controls = new ServiceRequestApprovalControls { CurrentPage = pageId } };

			_ps = InterfaceFactory.CreatePortfolioService();
			List<ServiceRequestTableItemModel> requests = new List<ServiceRequestTableItemModel>();

			var srList = _ps.GetServiceRequestsForRequestorId(UserId, UserId).ToList();      //for pagination
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
						PackageName = ServicePackageHelper.GetPackage(UserId, _ps, item.ServiceOptionId).Name,
						DateRequired = item.RequestedForDate,
						DateSubmitted = item.SubmissionDate
					});
				}
			}
			model.ServiceRequests = requests;
			return View(model);
		}

		/// <summary>
		/// Filter by state
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult FilterStatus(ServiceRequestState state, int pageId )
		{
			return View("Index");
		}

		public ActionResult FilterGroupStatus(ServiceRequestState state, int pageId)
		{
			return View("Index");
		}


		public ActionResult FilterGroupRequestor(int userId, int pageId)
		{
			return View("Index");
		}

		/// <summary>
		/// performs a ServiceRequest State change
		/// </summary>
		/// <param name="id"></param>
		/// <param name="state"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public ActionResult ChangeState(int id, ServiceRequestState state, string message)
		{
			int userId;              //user info
			try { userId = int.Parse(Session["Id"].ToString()); }     //login is very required
			catch (Exception) { return null; }

			try
			{
				switch (state)
				{
					case ServiceRequestState.Cancelled:
						_rm.CancelRequest(userId, id, message);
						break;
					case ServiceRequestState.Submitted:
						_rm.SubmitRequest(userId, id, message);
						break;
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to set Service Request to {state}, {exception}";
				return RedirectToAction("Index");
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully {state} Service Request";

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Generic state change screen, will adjust to the state
		/// </summary>
		/// <param name="id">Service Requeset Id</param>
		/// <param name="nextState">next SR state to change to</param>
		/// <returns></returns>
		public ActionResult ConfirmServiceRequestStateChange(int id, ServiceRequestState nextState)
		{
			ServiceRequestStateChangeModel model = new ServiceRequestStateChangeModel { NextState = nextState };

			_ps = InterfaceFactory.CreatePortfolioService();
			model.ServiceRequestModel = new ServiceRequestModel();
			model.ServiceRequestModel.ServiceRequest = _ps.GetServiceRequest(UserId, id);
			model.ServiceRequestModel.Package = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequestModel.ServiceOptionId);

			List<DisplayListModel> displayList = new List<DisplayListModel>();

			foreach (var category in model.ServiceRequestModel.Package.ServiceOptionCategoryTags)
			{
				DisplayListModel listItem = new DisplayListModel { Category = category.ServiceOptionCategory, Options = new List<DisplayListModelItem>() };

				foreach (var option in _ps.GetServiceOptionCategory(UserId, category.ServiceOptionCategoryId).ServiceOptions)
				{
					foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)
					{
						if (serviceRequestOption.ServiceOptionId == option.Id)
						{
							listItem.Options.Add(new DisplayListModelItem { ServiceOption = option, ServiceRequestOption = serviceRequestOption });

						}
					}
				}
				displayList.Add(listItem);
			}
			model.DisplayList = displayList;
			return View(model);
		}

		public int UserId
		{
			get
			{
				/*try*/ { return int.Parse(Session["Id"].ToString()); }                   //login is required
				//catch (Exception) { return RedirectToAction("Index", "UserAccount"); }  //some login issue
				//return int.Parse(Session["Id"].ToString());
			}
		}
	}
}