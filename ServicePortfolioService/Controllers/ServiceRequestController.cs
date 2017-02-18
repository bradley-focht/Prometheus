using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class ServiceRequestController : EntityController<IServiceRequestDto>, IServiceRequestController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceRequestController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceRequestController(int userId)
		{
			_userId = userId;
		}

		public IServiceRequestDto GetServiceRequest(int serviceRequestId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequest = context.ServiceRequests.Find(serviceRequestId);
				if (serviceRequest != null)
					return ManualMapper.MapServiceRequestToDto(serviceRequest);
				return null;
			}
		}

		public IServiceRequestDto ModifyServiceRequest(IServiceRequestDto serviceRequest, EntityModification modification)
		{
			return base.ModifyEntity(serviceRequest, modification);
		}

		protected override IServiceRequestDto Create(IServiceRequestDto serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequest = context.ServiceRequests.Find(serviceRequestDto.Id);
				if (serviceRequest != null)
				{
					throw new InvalidOperationException(string.Format("Service Request with ID {0} already exists.", serviceRequestDto.Id));
				}
				var saved = context.ServiceRequests.Add(ManualMapper.MapDtoToServiceRequest(serviceRequestDto));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceRequestToDto(saved);
			}
		}

		protected override IServiceRequestDto Update(IServiceRequestDto serviceRequestDto)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceRequestToDto(updated);
			}
		}

		protected override IServiceRequestDto Delete(IServiceRequestDto serviceRequestDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequests.Find(serviceRequestDto.Id);
				context.ServiceRequests.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}

		public IEnumerable<IServiceRequestDto> GetServiceRequestsForRequestorId(int requestorUserId)
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
	}
}