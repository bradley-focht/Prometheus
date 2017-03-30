using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using DataService;
using DataService.DataAccessLayer;
using UserManager;

namespace RequestService.Controllers
{
	public class ServiceRequestController : EntityController<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>>, IServiceRequestController
	{
		private readonly IUserManager _userManager;
		private readonly IRequestManager _requestManager;

		public ServiceRequestController(IUserManager userManager, IRequestManager requestManager)
		{
			_userManager = userManager;
			_requestManager = requestManager;
		}

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> GetServiceRequest(int performingUserId, int serviceRequestId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequest = context.ServiceRequests
					.Include(x => x.ServiceRequestOptions)
					.Include(x => x.ServiceRequestUserInputs)
					.Include(x => x.ServiceRequestOptions.Select(y => y.ServiceOption))
					.FirstOrDefault(x => x.Id == serviceRequestId);
				if (serviceRequest != null)
					return ManualMapper.MapServiceRequestToDto(serviceRequest);
				return null;
			}
		}

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ModifyServiceRequest(int performingUserId, IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> serviceRequest, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceRequest, modification);
		}

		protected override IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> Create(int performingUserId, IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequest = context.ServiceRequests.Find(serviceRequestDto.Id);
				if (serviceRequest != null)
				{
					throw new InvalidOperationException(string.Format("Service Request with ID {0} already exists.", serviceRequestDto.Id));
				}
				var saved = context.ServiceRequests.Add(ManualMapper.MapDtoToServiceRequest(serviceRequestDto));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceRequestToDto(saved);
			}
		}

		protected override IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> Update(int performingUserId, IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceRequests.Any(x => x.Id == serviceRequestDto.Id))
				{
					throw new InvalidOperationException(string.Format("Service Request with ID {0} cannot be updated since it does not exist.", serviceRequestDto.Id));
				}
				var updated = ManualMapper.MapDtoToServiceRequest(serviceRequestDto);
				context.ServiceRequests.Attach(updated);
				context.Entry(updated).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceRequestToDto(updated);
			}
		}

		protected override IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> Delete(int performingUserId, IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequests.Find(serviceRequestDto.Id);
				context.ServiceRequests.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}

		public IEnumerable<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>> GetServiceRequestsForRequestorId(int performingUserId, int requestorUserId)
		{
			using (var context = new PrometheusContext())
			{
				var userRequestestRequests = context.ServiceRequests.Where(x => x.RequestedByUserId == requestorUserId);
				foreach (var userRequestestRequest in userRequestestRequests)
				{
					yield return ManualMapper.MapServiceRequestToDto(userRequestestRequest);
				}
			}
		}

		public IEnumerable<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>> GetServiceRequestsForApproverId(int approverId)
		{
			if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveAnyRequests))
			{
				//All submitted requests
				using (var context = new PrometheusContext())
				{
					var serviceRequests = context.ServiceRequests;

					foreach (var serviceRequest in serviceRequests)
					{
						yield return ManualMapper.MapServiceRequestToDto(serviceRequest);
					}
				}
			}
			else if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveDepartmentRequests))
			{
				//Submitted requests with the same department ID as the approver
				using (var context = new PrometheusContext())
				{
					//Will never be null. UserHasPermission will catch that
					var approverDepartmentId = context.Users.Find(approverId).DepartmentId;

					var serviceRequests = context.ServiceRequests.Where(
							x => x.State == ServiceRequestState.Submitted && x.DepartmentId == approverDepartmentId);

					foreach (var serviceRequest in serviceRequests)
					{
						yield return ManualMapper.MapServiceRequestToDto(serviceRequest);
					}
				}
			}
			else if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveBasicRequests))
			{
				//Basic Requests that the approver submitted
				using (var context = new PrometheusContext())
				{
					var serviceRequests = context.ServiceRequests;

					foreach (var serviceRequest in serviceRequests)
					{
						yield return ManualMapper.MapServiceRequestToDto(serviceRequest);
					}
				}
			}
			/*
						throw new PermissionException("Cannot Approve Service Requests",
				approverId, ApproveServiceRequest.ApproveMinistryRequests);
				*/
		}

		protected override bool UserHasPermissionToModify(int performingUserId, IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request, EntityModification modification, out object permission)
		{
			permission = ServiceRequestSubmission.CanSubmitRequests;
			switch (modification)
			{
				case EntityModification.Create:
					return _userManager.UserHasPermission(performingUserId, (ServiceRequestSubmission)permission);
				case EntityModification.Update:
					return _requestManager.UserCanEditRequest(performingUserId, request.Id);
				case EntityModification.Delete:
					return _userManager.UserHasPermission(performingUserId, (ServiceRequestSubmission)permission);
			}
			return false;
		}

		public IEnumerable<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>> GetServiceRequests(int performingUserId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequests = context.ServiceRequests
					.Include(x => x.ServiceRequestOptions)
					.Include(x => x.ServiceRequestUserInputs)
					.Include(x => x.ServiceRequestOptions.Select(y => y.ServiceOption));
				var toReturn = serviceRequests.AsEnumerable().Select(x => ManualMapper.MapServiceRequestToDto(x));
				return toReturn.ToList();
			}
		}
	}
}
