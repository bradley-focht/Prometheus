using System;
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
		private readonly IPortfolioService _ps;
		private readonly IServiceRequestController _srController;
		private readonly IUserManager _um;
		private readonly IRequestManager _rm;

		public ServiceRequestApprovalController()
		{
			_rm = InterfaceFactory.CreateRequestManager();
			_ps = InterfaceFactory.CreatePortfolioService();
			_srController = InterfaceFactory.CreateServiceRequestController();
			_um = InterfaceFactory.CreateUserManagerService();
			try { _pageSize = ConfigHelper.GetPaginationSize(); }
			catch (Exception) { _pageSize = 12; }       //just in case
		}

		/// <summary>
		/// display the home/portal page
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(int pageId = 0)
		{	//default of my service requests filtered by incomplete
			ServiceRequestApprovalModel model = ServiceRequestApprovalHelper.GetMyRequests(_srController, UserId, pageId, _pageSize, ServiceRequestState.Incomplete);
			return View(model);
		}

		/// <summary>
		/// Filter My Service Requests by state
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult FilterStatus(ServiceRequestState state, int pageId = 0)
		{
			ServiceRequestApprovalModel model = ServiceRequestApprovalHelper.GetMyRequests(_srController, UserId, pageId, _pageSize, state);
			model.Controls.FilterAction = "FilterStatus";
			model.Controls.FilterStateRequired = true;
			return View("Index", model);
		}

		/// <summary>
		/// Filter My Requests, all (except cancelled)
		/// </summary>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult AllServiceRequests(int pageId = 0)
		{
			ServiceRequestApprovalModel model = ServiceRequestApprovalHelper.GetAllRequests(_srController, UserId, pageId, _pageSize);
			model.Controls.FilterAction = "AllServiceRequests";
			return View("Index", model);
		}

		/// <summary>
		/// Get all Department service requests
		/// </summary>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult AllDepartmentServiceRequests(int pageId = 0)
		{
			ServiceRequestApprovalModel model = ServiceRequestApprovalHelper.GetAllDepartmentRequests(_srController, UserId, pageId, _pageSize);
			model.Controls.FilterAction = "GetDepartmentRequests";
			return View("Index", model);
		}

		/// <summary>
		/// Filter by State
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult FilterDepartmentStatus(ServiceRequestState state, int pageId=0)
		{
			ServiceRequestApprovalModel model = ServiceRequestApprovalHelper.GetDepartmentRequests(_srController, UserId, pageId, _pageSize, state);
			model.Controls.FilterAction = "FilterDepartmentStatus";
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
						_rm.CancelRequest(UserId, id, message);
						break;
					case ServiceRequestState.Submitted:
						_rm.SubmitRequest(UserId, id);
						break;
					case ServiceRequestState.Approved:
						_rm.ApproveRequest(UserId, id, ApprovalResult.Approved, message);
						break;
					case ServiceRequestState.Denied:
						_rm.ApproveRequest(UserId, id, ApprovalResult.Denied, message);
						break;
					default:
						throw new Exception("State not implemented");	//incomplete is one of these
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
		/// Generic state change screen, will adjust to the state
		/// </summary>
		/// <param name="id">Service Requeset Id</param>
		/// <param name="nextState">next SR state to change to</param>
		/// <returns></returns>
		public ActionResult ConfirmServiceRequestStateChange(int id, ServiceRequestState nextState)
		{
			ServiceRequestStateChangeModel model = ServiceRequestSummaryHelper.CreateStateChangeModel(_ps, _srController, nextState, UserId, id);
			
			return View(model);
		}

		/// <summary>
		/// Show a single SR summary with all available next actions
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceRequest(int id)
		{
			ServiceRequestStateChangeModel model = ServiceRequestSummaryHelper.CreateStateChangeModel(_ps, UserId, _srController, id);
			return View("ConfirmServiceRequestStateChange", model);
		}
	}
}