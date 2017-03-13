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
	public class ServiceController : EntityController<IServiceDto>, IServiceController
	{
		public IServiceDto GetService(int serviceId)
		{
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(serviceId);

				return ManualMapper.MapServiceToDto(service);
			}
		}

		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				return context.Services.Where(x => x.ServiceBundleId == serviceBundleId).Select(x => ManualMapper.MapServiceToDto(x));
			}
		}

		public IServiceDto ModifyService(int performingUserId, IServiceDto service, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, service, modification);
		}

		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			var services = GetServicesForServiceBundle(serviceBundleId);
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(services.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);
		}

		public IEnumerable<Tuple<int, string>> GetServiceNames()
		{
			using (var context = new PrometheusContext())
			{
				var serviceNames = context.Services;

				//Empty list
				if (!serviceNames.Any())                    //return an empty list instead of null
					return new List<Tuple<int, string>>();

				var statuses = new List<ServiceDto>();
				foreach (var status in serviceNames)        //Maping and Linq don't seem to work together
					statuses.Add(ManualMapper.MapServiceToDto(status));

				var nameList = new List<Tuple<int, string>>();

				nameList.AddRange(statuses.Select(x => new Tuple<int, string>(x.Id, x.Name)));
				return nameList.OrderBy(x => x.Item2);
			}
		}

		public IEnumerable<IServiceDto> GetServices()
		{
			using (var context = new PrometheusContext())
			{
				var serviceNames = context.Services;
				var serviceList = new List<ServiceDto>();

				//Empty list
				if (!serviceNames.Any())
					return serviceList;

				foreach (var status in serviceNames)        //seem to need to do this without LINQ
					serviceList.Add(ManualMapper.MapServiceToDto(status));

				return serviceList.OrderBy(s => s.Name);
			}
		}

		public IEnumerable<IServiceDocumentDto> GetServiceDocuments(int serviceId)
		{
			var service = GetService(serviceId);
			return service.ServiceDocuments;
		}

		protected override IServiceDto Create(int performingUserId, IServiceDto service)
		{
			using (var context = new PrometheusContext())
			{
				var existingService = context.Services.Find(service.Id);
				if (existingService == null)
				{
					var savedService = context.Services.Add(ManualMapper.MapDtoToService(service));
					context.SaveChanges(performingUserId);
					return ManualMapper.MapServiceToDto(savedService);
				}
				else
				{
					throw new InvalidOperationException(string.Format("Service with ID {0} already exists.", service.Id));
				}
			}
		}

		protected override IServiceDto Update(int performingUserId, IServiceDto service)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Services.Any(x => x.Id == service.Id))
				{
					throw new InvalidOperationException("Service record must exist in order to be updated.");
				}
				var updatedService = ManualMapper.MapDtoToService(service);

				context.Services.Attach(updatedService);
				context.Entry(updatedService).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceToDto(updatedService);
			}
		}

		protected override IServiceDto Delete(int performingUserId, IServiceDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Services.Find(entity.Id);

				var tagsToDelete = context.ServiceTags.Where(x => x.ServiceId == entity.Id);
				context.ServiceTags.RemoveRange(tagsToDelete);
				context.SaveChanges(performingUserId);

				context.Services.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}

		public IEnumerable<IServiceDto> SetServiceBundleForServices(int performingUserId, int? serviceBundleId, IEnumerable<IServiceDto> services)
		{
			using (var context = new PrometheusContext())
			{
				foreach (var serviceDto in services)
				{
					var service = context.Services.Find(serviceDto.Id);
					service.ServiceBundleId = serviceBundleId;

					context.Services.Attach(service);
					context.Entry(service).State = EntityState.Modified;
					context.SaveChanges(performingUserId);

					yield return ManualMapper.MapServiceToDto(service);
				}
			}
		}
	}
}
