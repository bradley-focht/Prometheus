using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using UserManager;

namespace RequestService.Controllers
{
	public class ServiceRequestController : EntityController<IServiceRequestDto>, IServiceRequestController
	{
		private readonly IUserManager _userManager;
		public ServiceRequestController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		public IServiceRequestDto GetServiceRequest(int performingUserId, int serviceRequestId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequest = context.ServiceRequests.Find(serviceRequestId);
				if (serviceRequest != null)
					return ManualMapper.MapServiceRequestToDto(serviceRequest);
				return null;
			}
		}

		public IServiceRequestDto ModifyServiceRequest(int performingUserId, IServiceRequestDto serviceRequest, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceRequest, modification);
		}

		protected override IServiceRequestDto Create(int performingUserId, IServiceRequestDto serviceRequestDto)
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

		protected override IServiceRequestDto Update(int performingUserId, IServiceRequestDto serviceRequestDto)
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

		protected override IServiceRequestDto Delete(int performingUserId, IServiceRequestDto serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequests.Find(serviceRequestDto.Id);
				context.ServiceRequests.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}

		public IEnumerable<IServiceRequestDto> GetServiceRequestsForRequestorId(int performingUserId, int requestorUserId)
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

		public IEnumerable<IServiceRequestDto> GetServiceRequestsForApproverId(int approverId)
		{
			using (var context = new PrometheusContext())
			{

				if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveAnyRequests))
				{
					//Get all approvable requests
					return context.ServiceRequests.Where(x => x.State == ServiceRequestState.Submitted)
							.Select(x => ManualMapper.MapServiceRequestToDto(x));
				}

				if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveMinistryRequests))
				{
					//Will never be null. UserHasPermission will catch that
					var approverDepartmentId = context.Users.Find(approverId).DepartmentId;

					return context.ServiceRequests.Where(x => x.State == ServiceRequestState.Submitted && x.DepartmentId == approverDepartmentId)
							.Select(x => ManualMapper.MapServiceRequestToDto(x));
				}

				//TODO: Still requires discussion
				if (_userManager.UserHasPermission(approverId, ApproveServiceRequest.ApproveBasicRequests))
				{
					return new List<IServiceRequestDto>();
				}

				throw new PermissionException("Cannot Approve Service Requests",
					approverId, ApproveServiceRequest.ApproveMinistryRequests);
			}
		}
	}
}