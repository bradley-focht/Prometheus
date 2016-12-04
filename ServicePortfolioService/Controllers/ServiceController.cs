using AutoMapper;
using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class ServiceController : IServiceController
	{
		private int _userId;
		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceController(int userId)
		{
			_userId = userId;
		}

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
				var serviceBundle = context.ServiceBundles.Find(serviceBundleId);
				return serviceBundle.Services.Select(x => Mapper.Map<ServiceDto>(x));
			}
		}

		public IServiceDto ModifyService(IServiceDto service, EntityModification modification)
		{
			switch (modification)
			{
				case EntityModification.Create:
					return SaveService(service);
				case EntityModification.Update:
					return UpdateService(service);
				case EntityModification.Delete:
					return DeleteService(service.Id) ? null : service;
			}
			throw new ModificationException(string.Format("Modification {0} was not performed on entity {1}", modification, service));
		}

		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			var services = GetServicesForServiceBundle(serviceBundleId);
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(services.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);
		}

		private IServiceDto SaveService(IServiceDto service)
		{
			using (var context = new PrometheusContext())
			{
				var existingService = context.Services.Find(service.Id);
				if (existingService == null)
				{
					//var savedService = context.Services.Add(Mapper.Map<Service>(service));
					var savedService = context.Services.Add(ManualMapper.MapDtoToService(service));
					context.SaveChanges(_userId);
					return ManualMapper.MapServiceToDto(savedService);
					//return Mapper.Map<ServiceDto>(savedService);
				}
				else
				{
					return UpdateService(service);
				}
			}
		}

		private IServiceDto UpdateService(IServiceDto service)
		{
			using (var context = new PrometheusContext())
			{
				//var existingService = context.Services.Find(service.Id);
				//if (existingService == null)
				//{
				//	throw new InvalidOperationException("Serivce record must exist in order to be updated.");
				//}
				//else
				//{
				var updatedService = ManualMapper.MapDtoToService(service);
				//context.Services.Attach(updatedService);
				context.Entry(updatedService).State = EntityState.Modified;
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceToDto(updatedService);
				//}
			}
		}

		private bool DeleteService(int serviceId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Services.Find(serviceId);
				context.Services.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return true;
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

		public IServiceDocumentDto ModifyServiceDocument(IServiceDocumentDto document, EntityModification modification)
		{
			switch (modification)
			{
				case EntityModification.Create:
					return SaveServiceDocument(document);
				case EntityModification.Update:
					return UpdateServiceDocument(document);
				case EntityModification.Delete:
					return DeleteServiceDocument(document.StorageNameGuid) ? null : document;
			}
			throw new ModificationException(string.Format("Modification {0} was not performed on entity {1}", modification, document));
		}

		private bool DeleteServiceDocument(Guid documentGuid)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == documentGuid);
				context.ServiceDocuments.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return true;
		}

		private IServiceDocumentDto UpdateServiceDocument(IServiceDocumentDto document)
		{
			throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Documents.", EntityModification.Update));
		}

		public IServiceDocumentDto SaveServiceDocument(IServiceDocumentDto document)
		{
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(document.ServiceId);            //this is going to add only, it isn't going to update existing records 
				service.ServiceDocuments.Add(ManualMapper.MapDtoToServiceDocument(document));
				context.SaveChanges(_userId);

				return new ServiceDocumentDto();
			}
		}

		public IServiceDocumentDto GetServiceDocument(Guid documentGuid)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == documentGuid);
				return ManualMapper.MapServiceDocumentToDto(document);
			}
		}
	}
}
