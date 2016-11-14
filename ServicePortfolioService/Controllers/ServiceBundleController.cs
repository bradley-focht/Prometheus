using AutoMapper;
using Common.Dto;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
				var serviceBundle = context.Services.Find(serviceBundleId);
				return Mapper.Map<ServiceBundleDto>(serviceBundle);
			}
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			using (var context = new PrometheusContext())
			{
				var serviceBundles = context.ServiceBundles;

				//Don't attempt query if there is no data
				if (!serviceBundles.Any())
					return null;

				return serviceBundles.Select(x => Mapper.Map<ServiceBundleDto>(x));
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
				var existingServiceBundle = context.ServiceBundles.Find(serviceBundle.Id);
				if (existingServiceBundle == null)
				{
					//TODO: Sean - it throws an exception here that Mapper is not configured. I don't know where configuration of
					// mapper is done
					var savedServiceBundle = context.ServiceBundles.Add(Mapper.Map<ServiceBundle>(serviceBundle));
					context.SaveChanges(_userId);
					return Mapper.Map<ServiceBundleDto>(savedServiceBundle);
				}
				else
				{
					return UpdateServiceBundle(serviceBundle);
				}
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
