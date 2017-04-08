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
		/// <summary>
		/// Finds service with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public IServiceDto GetService(int serviceId)
		{
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Include(x => x.ServiceDocuments).FirstOrDefault(x => x.Id == serviceId);
				return ManualMapper.MapServiceToDto(service);
			}
		}

		/// <summary>
		/// Finds the service bundle from identifier then returns all of its services
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var toReturn = new List<IServiceDto>();
				var services = context.Services.Where(x => x.ServiceBundleId == serviceBundleId);
				foreach (var service in services)
				{
					toReturn.Add(ManualMapper.MapServiceToDto(service));
				}
				return toReturn;
			}
		}

		/// <summary>
		/// Modifies the service in the database
		/// </summary>
		/// <param name="performingUserId">ID for user doing modification</param>
		/// <param name="service"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified entity DTO</returns>
		public IServiceDto ModifyService(int performingUserId, IServiceDto service, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, service, modification);
		}

		/// <summary>
		/// Finds the service bundle from identifier then uses its services
		/// KVP of all services IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			var services = GetServicesForServiceBundle(serviceBundleId);
			return services.Select(x => new Tuple<int, string>(x.Id, x.Name)).OrderBy(x => x.Item2);
		}

		/// <summary>
		/// Gets a list of services and names for making lists
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Get a full list of services 
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Get all documents associated with a service
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Applies a service bundle ID to multiple services.
		/// 
		/// NOTE: null can be applied as service bundle ID to remove the services from their bundle
		/// </summary>
		/// <param name="performingUserId">ID for user performing adjustment</param>
		/// <param name="serviceBundleId">ID to add to the services. Can be null</param>
		/// <param name="services">Services to set the Service Bundle on</param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> SetServiceBundleForServices(int performingUserId, int? serviceBundleId, IEnumerable<IServiceDto> services)
		{
			using (var context = new PrometheusContext())
			{
				var servicesUpdated = new List<IServiceDto>();
				foreach (var serviceDto in services)
				{
					var service = context.Services.Find(serviceDto.Id);
					service.ServiceBundleId = serviceBundleId;

					context.Services.Attach(service);
					context.Entry(service).State = EntityState.Modified;
					context.SaveChanges(performingUserId);

					servicesUpdated.Add(ManualMapper.MapServiceToDto(service));
				}
				return servicesUpdated;
			}
		}
	}
}
