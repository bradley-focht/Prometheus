using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Enums;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using RequestService;
using RequestService.Controllers;
using ServicePortfolioService;
using UserManager;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class ServiceRequestApprovalController : PrometheusController
	{
		private readonly int _pageSize;
		private readonly IPortfolioService _portfolioService;
		private readonly IServiceRequestController _serviceRequestController;
		private readonly IRequestManager _requestManager;

		public ServiceRequestApprovalController(IPortfolioService portfolioService, IServiceRequestController serviceRequestController,
			IRequestManager requestManager, IUserManager userManager)
		{
			_portfolioService = portfolioService;
			_serviceRequestController = serviceRequestController;
			_requestManager = requestManager;
			_userManager = userManager;

			try { _pageSize = ConfigHelper.GetPaginationSize(); }
			catch (Exception) { _pageSize = 12; }       //just in case
		}

		/// <summary>
		/// display the home/portal page
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(int pageId = 0)
		{   //default of my service requests filtered by incomplete
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel();
			try
			{
				model = ServiceRequestApprovalHelper.GetMyRequests(_serviceRequestController, _userManager, UserId, pageId, _pageSize, ServiceRequestState.Incomplete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve any service requests, {exception.Message}";
			}
			if (model.Controls == null)
			{
				model.Controls = new ServiceRequestApprovalControls();
			}
			return View("Index", model);
		}

		/// <summary>
		/// Filter My Service Requests by state
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult FilterStatus(ServiceRequestState state, int pageId = 0)
		{
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel();
			try
			{
				model = ServiceRequestApprovalHelper.GetMyRequests(_serviceRequestController, _userManager, UserId, pageId, _pageSize, state);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve any service requests, {exception.Message}";
			}
			if (model.Controls != null)
			{
				model.Controls.FilterAction = "FilterStatus";
				model.Controls.FilterStateRequired = true;
			}
			else
			{
				model.Controls = new ServiceRequestApprovalControls();
			}
			return View("Index", model);
		}

		/// <summary>
		/// Filter My Requests, all (except cancelled)
		/// </summary>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult AllServiceRequests(int pageId = 0)
		{
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel();
			try
			{
				model = ServiceRequestApprovalHelper.GetAllRequests(_serviceRequestController, _userManager, UserId, pageId, _pageSize);			
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve any service requests, {exception.Message}";
			}
			if (model.Controls != null)
			{
				model.Controls.FilterAction = "AllServiceRequests";
			}
			else
			{
				model.Controls = new ServiceRequestApprovalControls();
			}
			return View("Index", model);
		}

		/// <summary>
		/// Get all Department service requests
		/// </summary>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult AllDepartmentServiceRequests(int pageId = 0)
		{
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel();
			try
			{
				model = ServiceRequestApprovalHelper.GetAllDepartmentRequests(_serviceRequestController, _userManager, UserId,
					pageId, _pageSize);
				
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve any service requests, {exception.Message}";
			}
			if (model.Controls != null)
			{
				model.Controls.FilterAction = "AllDepartmentServiceRequests";
			}
			else
			{
				model.Controls = new ServiceRequestApprovalControls();
			}
			return View("Index", model);
		}

		/// <summary>
		/// Filter by State
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult FilterDepartmentStatus(ServiceRequestState state, int pageId = 0)
		{
			ServiceRequestApprovalModel model = new ServiceRequestApprovalModel();
			try
			{
				model = ServiceRequestApprovalHelper.GetDepartmentRequests(_serviceRequestController, _userManager, UserId, pageId, _pageSize, state);		
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve any service requests, {exception.Message}";
			}
			if (model.Controls != null) { 
			model.Controls.FilterAction = "FilterDepartmentStatus";
			}
			else
			{
				model.Controls = new ServiceRequestApprovalControls();
			}
			return View("Index", model);
		}

		/// <summary>
		/// performs a ServiceRequest State change
		/// </summary>
		/// <param name="id"></param>
		/// <param name="state"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ChangeState(int id, ServiceRequestState state, string message)
		{
			try
			{
				switch (state)
				{
					case ServiceRequestState.Cancelled:
						_requestManager.CancelRequest(UserId, id, message);
						break;
					case ServiceRequestState.Submitted:
						_requestManager.SubmitRequest(UserId, id);
						break;
					case ServiceRequestState.Approved:
						_requestManager.ApproveRequest(UserId, id, ApprovalResult.Approved, message);
						break;
					case ServiceRequestState.Denied:
						_requestManager.ApproveRequest(UserId, id, ApprovalResult.Denied, message);
						break;
					default:
						throw new Exception("State not implemented");   //incomplete is one of these
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to set Service Request to {state}, {exception}";
				return RedirectToAction("Index");
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully set Service Request to {state}";

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Show a single SR summary with all available next actions
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceRequest(int id)
		{
			ServiceRequestStateChangeModel model = ServiceRequestSummaryHelper.CreateStateChangeModel(_portfolioService, UserId, _serviceRequestController, id);
			if (model.ServiceRequestModel == null) return View("ConfirmServiceRequestStateChange", model);

			//check for an sr approval
			IApprovalController approvalController = new ApprovalController();
			try
			{
				model.ServiceRequestModel.Approval = approvalController.GetApprovalForServiceRequest(UserId, id);
			}
			catch (Exception) { }
			//resolve display names to Guids
			//requestees
			if (model.ServiceRequestModel.ServiceRequest.RequestedForGuids != null)
			{
				List<string> displayNames = new List<string>();
				foreach (var userGuidstring in model.ServiceRequestModel.ServiceRequest.RequestedForGuids.Split(','))
				{
					if (userGuidstring != "")   //get names from ad 
					{
						try
						{
							displayNames.Add(_userManager.GetDisplayName(Guid.Parse(userGuidstring)));
						}
						catch (Exception)
						{
							displayNames.Add("Name not found");
						}
					}
				}
				model.ServiceRequestModel.RequesteeDisplayNames = displayNames.OrderBy(x => x);
			}
			//requestor
			try
			{
				model.ServiceRequestModel.RequestorDisplayName = _userManager.GetDisplayName(_userManager.GetUser(UserId, model.ServiceRequestModel.Requestor).AdGuid);
			}
			catch (Exception)
			{
				model.ServiceRequestModel.RequestorDisplayName = "Name not found";
			}
		// business logic 
		model.CanEditServiceRequest = _requestManager.UserCanEditRequest(UserId, model.ServiceRequestModel.ServiceRequest.Id);
			model.CanEditServiceRequest = _requestManager.UserCanEditRequest(UserId, model.ServiceRequestModel.ServiceRequest.Id);
			model.AvailableStates = _requestManager.ValidStates(UserId, id);
			return View("ConfirmServiceRequestStateChange", model);
		}
	}
}