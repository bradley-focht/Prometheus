using AutoMapper;
using Common.Dto;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService;

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
				return Mapper.Map<ServiceDto>(service);
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

		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			var services = this.GetServicesForServiceBundle(serviceBundleId);
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(services.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);
		}

        //TODO: man mapper is in here too
		public IServiceDto SaveService(IServiceDto service)
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
				var existingService = context.Services.Find(service.Id);
				if (existingService == null)
				{
					throw new InvalidOperationException("Serivce record must exist in order to be updated.");
				}
				else
				{
					var updatedService = Mapper.Map<Service>(service);
					context.Services.Attach(updatedService);
					context.Entry(updatedService).State = EntityState.Modified;
					context.SaveChanges(_userId);
					return Mapper.Map<ServiceDto>(updatedService);
				}
			}
		}

		public bool DeleteService(int serviceId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Services.Find(serviceId);
				context.Services.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return true;
		}
	}
}
