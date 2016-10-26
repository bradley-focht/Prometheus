using AutoMapper;
using Common.Dto;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolio.Controllers
{
	internal class ServiceBundleController : IServiceBundleController
	{
		public IServiceBundleDto GetServiceBundle(int userId, int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceBundle = context.Services.Find(serviceBundleId);
				return Mapper.Map<ServiceBundleDto>(serviceBundle);
			}
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles(int userId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceBundles = context.ServiceBundles;
				return serviceBundles.Select(x => Mapper.Map<ServiceBundleDto>(x));
			}
		}

		public IEnumerable<Tuple<int, string>> GetServiceBundleNames(int userId)
		{
			var bundles = GetServiceBundles(userId);
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(bundles.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);
		}

		public IServiceBundleDto SaveServiceBundle(int userId, IServiceBundleDto serviceBundle)
		{
			using (var context = new PrometheusContext())
			{
				var existingServiceBundle = context.ServiceBundles.Find(serviceBundle.Id);
				if (existingServiceBundle == null)
				{
					var savedServiceBundle = context.ServiceBundles.Add(Mapper.Map<ServiceBundle>(serviceBundle));
					context.SaveChanges();
					return Mapper.Map<ServiceBundleDto>(savedServiceBundle);
				}
				else
				{
					return UpdateServiceBundle(userId, serviceBundle);
				}
			}
		}

		private IServiceBundleDto UpdateServiceBundle(int userId, IServiceBundleDto serviceBundle)
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
					context.SaveChanges();
					return Mapper.Map<ServiceBundleDto>(updatedServiceBundle);
				}
			}
		}

		public bool DeleteServiceBundle(int userId, int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceBundles.Find(serviceBundleId);
				context.ServiceBundles.Remove(toDelete);
				context.SaveChanges();
			}
			return true;
		}
	}
}
