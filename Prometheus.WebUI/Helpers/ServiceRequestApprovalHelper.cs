﻿using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using RequestService.Controllers;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// Handles models and data filtering for ServiceRequestApproval controller
	/// </summary>
	public class ServiceRequestApprovalHelper
	{
		/// <summary>
		/// Returns all service request approval model for the user Id, filtered, and ordered by id
		/// </summary>
		/// <param name="srController">service request controller</param>
		/// <param name="userId">requesting user</param>
		/// <param name="currentPage">current page</param>
		/// <param name="pageSize">page size</param>
		/// <param name="state">state to filter by</param>
		/// <returns></returns>
		public static ServiceRequestApprovalModel GetMyRequests(IServiceRequestController srController, int userId, int currentPage, int pageSize, ServiceRequestState state)
		{
			var model = new ServiceRequestApprovalModel { Controls = new ServiceRequestApprovalControls() };
			// retrieve filtered data
			var srList = (from s in srController.GetServiceRequestsForRequestorId(userId, userId)
						  where s.State == state && s.State != ServiceRequestState.Cancelled
						  orderby s.Id
						  select s).ToList();

			model.ServiceRequests = ConvertToTableModel(srList);
			Paginate(model, currentPage, pageSize);

			model.Controls.FilterText = $"Filtered My Service Requests by {state}";

			return model;
		}

		/// <summary>
		/// Returns all service request approval model for the user Id, ordered by Id
		/// </summary>
		/// <param name="srController">service request controller</param>
		/// <param name="userId">requesting user</param>
		/// <param name="currentPage">current page</param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static ServiceRequestApprovalModel GetAllRequests(IServiceRequestController srController, int userId, int currentPage,
			int pageSize)
		{
			var model = new ServiceRequestApprovalModel { Controls = new ServiceRequestApprovalControls() };
			var srList = (from s in srController.GetServiceRequestsForRequestorId(userId, userId) where s.State != ServiceRequestState.Cancelled orderby s.Id select s).ToList();
			model.ServiceRequests = ConvertToTableModel(srList);
			Paginate(model, currentPage, pageSize);

			model.Controls.FilterText = "All My Service Requests";

			return model;
		}

		/// <summary>
		/// Get all Department requests except cancelled, for an approver
		/// </summary>
		/// <param name="srController"></param>
		/// <param name="userId"></param>
		/// <param name="currentPage"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static ServiceRequestApprovalModel GetAllDepartmentRequests(IServiceRequestController srController, int userId,
			int currentPage, int pageSize)
		{
			var model = new ServiceRequestApprovalModel {Controls = new ServiceRequestApprovalControls()};
			var srList = (from s in srController.GetServiceRequestsForApproverId(userId)
				where s.State != ServiceRequestState.Cancelled
				orderby s.Id
				select s).ToList();
			model.ServiceRequests = ConvertToTableModel(srList);
			Paginate(model, currentPage, pageSize);
			

			return model;
		}

		/// <summary>
		/// Get Department Request approvals and filter
		/// </summary>
		/// <param name="srController"></param>
		/// <param name="userId"></param>
		/// <param name="currentPage"></param>
		/// <param name="pageSize"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public static ServiceRequestApprovalModel GetDepartmentRequests(IServiceRequestController srController, int userId,
			int currentPage, int pageSize, ServiceRequestState state)
		{
			var model = new ServiceRequestApprovalModel { Controls = new ServiceRequestApprovalControls() };
			var srList = (from s in srController.GetServiceRequestsForApproverId(userId)
						  where s.State != ServiceRequestState.Cancelled
						  orderby s.Id
						  select s).ToList();
			model.ServiceRequests = ConvertToTableModel(srList);
			Paginate(model, currentPage, pageSize);

			return model;
		}

		/// <summary>
		/// Filter to only current page and add pagination data to controls
		/// </summary>
		/// <param name="model"></param>
		/// <param name="currentPage"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static ServiceRequestApprovalModel Paginate(ServiceRequestApprovalModel model, int currentPage, int pageSize)
		{
			model.Controls.CurrentPage = currentPage;   //set current page

			if (model.ServiceRequests.Count > pageSize)
			{
				model.Controls.TotalPages = (model.ServiceRequests.Count + pageSize - 1) / pageSize;    //number of pages
				model.ServiceRequests = model.ServiceRequests.Skip(pageSize * currentPage).Take(pageSize).ToList(); //select current page
			}
			return model;

		}

		/// <summary>
		/// Put data into model to be displayed into a table
		/// </summary>
		public static List<ServiceRequestTableItemModel> ConvertToTableModel(IEnumerable<IServiceRequestDto> list)
		{
			List<ServiceRequestTableItemModel> requests = new List<ServiceRequestTableItemModel>();

			foreach (var item in list)
			{
				requests.Add(new ServiceRequestTableItemModel
				{
					Id = item.Id,
					State = item.State,
					Name = item.Name, //ServicePackageHelper.GetPackage(UserId, _ps, item.ServiceOptionId).Name,
					DateRequired = item.RequestedForDate,
					DateSubmitted = item.SubmissionDate
				});
			}
			return requests;
		}
	}
}