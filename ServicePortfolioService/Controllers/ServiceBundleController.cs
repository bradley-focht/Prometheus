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
	public class ServiceBundleController : EntityController<IServiceBundleDto>, IServiceBundleController
	{
		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceBundle = context.ServiceBundles.Find(serviceBundleId);
				//return Mapper.Map<ServiceBundleDto>(serviceBundle);   //stack overflow
				return ManualMapper.MapServiceBundleToDto(serviceBundle);
			}
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			using (var context = new PrometheusContext())
			{
				//TODO: Sean i changed this. it was selecting data from the context then returning it but when the receiver got the data, the data was disposed
				var serviceBundles = context.ServiceBundles;
				List<ServiceBundleDto> bundleDtos = new List<ServiceBundleDto>();


				foreach (var bundle in serviceBundles)
				{
					bundleDtos.Add(ManualMapper.MapServiceBundleToDto(bundle)); //LINQ & mappers of any sort are not friends. Doing this the old fashioned way. 
				}
				return bundleDtos;
			}
		}

		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{

			var bundles = GetServiceBundles();
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(bundles.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);

		}

		public IServiceBundleDto ModifyServiceBundle(int performingUserId, IServiceBundleDto serviceBundle, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceBundle, modification);
		}

		protected override IServiceBundleDto Create(int performingUserId, IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
				var bundle = context.ServiceBundles.Find(serviceBundle.Id);
				if (bundle != null)
				{
					throw new InvalidOperationException(string.Format("Service Bundle with ID {0} already exists.", serviceBundle.Id));
				}
				var savedBundle = context.ServiceBundles.Add(ManualMapper.MapDtoToServiceBundle(serviceBundle));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceBundleToDto(savedBundle);
			}
		}

		protected override IServiceBundleDto Update(int performingUserId, IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceBundles.Any(x => x.Id == serviceBundle.Id))
				{
					throw new InvalidOperationException("Service Bundle must exist in order to be updated.");
				}
				var updatedServiceBundle = ManualMapper.MapDtoToServiceBundle(serviceBundle);
				context.ServiceBundles.Attach(updatedServiceBundle);
				context.Entry(updatedServiceBundle).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceBundleToDto(updatedServiceBundle);
			}
		}

		protected override IServiceBundleDto Delete(int performingUserId, IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
				//Remove references to Service Bundle
				var servicesToUpdate = context.Services.Where(x => x.ServiceBundleId == serviceBundle.Id);
				foreach (var service in servicesToUpdate)
				{
					service.ServiceBundleId = null;

					context.Services.Attach(service);
					context.Entry(service).State = EntityState.Modified;
					context.SaveChanges(performingUserId);
				}

				//Delete the Bundle itself
				var toDelete = context.ServiceBundles.Find(serviceBundle.Id);
				context.ServiceBundles.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}
