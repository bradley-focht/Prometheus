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
	public class ServiceBundleController : IServiceBundleController
	{
		private int _userId;
		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceBundleController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceBundleController(int userId)
		{
			_userId = userId;
		}

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

		public IServiceBundleDto SaveServiceBundle(IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
                //TODO: Sean i commented out lots of stuff here like lots. Like a lot a lot. 
				//var existingServiceBundle = context.ServiceBundles.Find(serviceBundle.Id);
				//if (existingServiceBundle == null)
				//{

					//var savedServiceBundle = context.ServiceBundles.Add(Mapper.Map<ServiceBundle>(serviceBundle));
				    var savedServiceBundle = context.ServiceBundles.Add(ManualMapper.MapDtoToServiceBundle(serviceBundle));
					context.SaveChanges(_userId);
					//return Mapper.Map<ServiceBundleDto>(savedServiceBundle);
                    return ManualMapper.MapServiceBundleToDto(savedServiceBundle);
				//}
				//else
				//{
				//	return UpdateServiceBundle(serviceBundle);
				//}
			}
		}

		private IServiceBundleDto UpdateServiceBundle(IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
				var existingServiceBundle = context.ServiceBundles.Find(serviceBundle.Id);
				if (existingServiceBundle == null)
				{
					throw new InvalidOperationException("Service record must exist in order to be updated.");
				}
				else
				{
					var updatedServiceBundle = Mapper.Map<ServiceBundle>(serviceBundle);
					context.ServiceBundles.Attach(updatedServiceBundle);
					context.Entry(updatedServiceBundle).State = EntityState.Modified;
					context.SaveChanges(_userId);
					return Mapper.Map<ServiceBundleDto>(updatedServiceBundle);
				}
			}
		}

		public bool DeleteServiceBundle(int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceBundles.Find(serviceBundleId);
				context.ServiceBundles.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return true;
		}
	}
}
