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
		/// <summary>
		/// Finds service bundle with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceBundle = context.ServiceBundles.Find(serviceBundleId);
				return ManualMapper.MapServiceBundleToDto(serviceBundle);
			}
		}

		/// <summary>
		/// Returns all service bundles
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			using (var context = new PrometheusContext())
			{
				foreach (var bundle in context.ServiceBundles)
				{
					yield return ManualMapper.MapServiceBundleToDto(bundle);
				}

			}
		}

		/// <summary>
		/// KVP of all service bundle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{

			var bundles = GetServiceBundles();
			var nameList = new List<Tuple<int, string>>();
			nameList.AddRange(bundles.Select(x => new Tuple<int, string>(x.Id, x.Name)));
			return nameList.OrderBy(x => x.Item2);

		}

		/// <summary>
		/// Modifies the service bundle in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceBundle"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Bundle</returns>
		public IServiceBundleDto ModifyServiceBundle(int performingUserId, IServiceBundleDto serviceBundle, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceBundle, modification);
		}

		/// <summary>
		/// Creates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User creating the entity</param>
		/// <param name="entity">Entity to be created</param>
		/// <returns>Created entity DTO</returns>
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

		/// <summary>
		/// Updates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User updating the entity</param>
		/// <param name="entity">Entity to be updated</param>
		/// <returns>Updated entity DTO</returns>
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

		/// <summary>
		/// Deletes the entity in the database
		/// </summary>
		/// <param name="performingUserId">User deleting the entity</param>
		/// <param name="entity">Entity to be deleted</param>
		/// <returns>Deleted entity. null if deletion was successfull</returns>
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
