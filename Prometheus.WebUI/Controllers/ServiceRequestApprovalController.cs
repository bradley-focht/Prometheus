﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Enums;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using RequestService;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceRequestApprovalController : PrometheusController
	{
		private IPortfolioService _ps;
		private readonly int _pageSize;
		private IRequestManager _rm;

		public ServiceRequestApprovalController()
		{
			_rm = InterfaceFactory.CreateRequestManager();
			_ps = InterfaceFactory.CreatePortfolioService();
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

			
			List<ServiceRequestTableItemModel> requests = new List<ServiceRequestTableItemModel>();

			var srList = _ps.GetServiceRequestsForRequestorId(UserId, UserId).ToList();      //for pagination
			if (srList.Count > _pageSize)
			{
				model.Controls.TotalPages = (srList.Count + _pageSize - 1) / _pageSize;
				srList = srList.Skip(_pageSize * pageId).Take(_pageSize).ToList();
			}

			foreach (var item in srList)
			{

					requests.Add(new ServiceRequestTableItemModel
					{
						Id = item.Id,
						State = item.State,
						PackageName =  "SR", //ServicePackageHelper.GetPackage(UserId, _ps, item.ServiceOptionId).Name,
						DateRequired = item.RequestedForDate,
						DateSubmitted = item.SubmissionDate
					});
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
			try
			{
				switch (state)
				{
					case ServiceRequestState.Cancelled:
						_rm.CancelRequest(UserId, id, message);
						break;

					case ServiceRequestState.Submitted:
						_rm.SubmitRequest(UserId, id, message);
						break;
					case ServiceRequestState.Approved:
						_rm.ApproveRequest(UserId, id, ApprovalResult.Approved, message);
						break;
					case ServiceRequestState.Denied:
						_rm.ApproveRequest(UserId, id, ApprovalResult.Denied, message);
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
			ServiceRequestStateChangeModel model = ServiceRequestSummaryHelper.CreateStateChangeModel(_ps, nextState, UserId, id);

			return View(model);
		}

		
	}
}