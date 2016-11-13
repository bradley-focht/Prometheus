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
                //TODO: Sean - I changed the code here to keep it from crashing when there are no service bundles to query
                if (!serviceBundles.Any())   //don't attempt LINQ query if there is no data
                    return null;

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
                   //TODO: Sean - it throws an exception here that Mapper is not configured. I don't know where configuration of
                   // mapper is done
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
