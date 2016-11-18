using AutoMapper;
using Common.Dto;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Utilities;
using ServicePortfolioService.AutoMapperConfig;

namespace ServicePortfolioService.Controllers
{
	public class LifecycleStatusController : ILifecycleStatusController
	{
		private int _userId;
		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public LifecycleStatusController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public LifecycleStatusController(int userId)
		{
			_userId = userId;
		}

		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			using (var context = new PrometheusContext())
			{
				var lifecycleStatusRecords = context.LifecycleStatuses;

				//Empty list
				if (!lifecycleStatusRecords.Any())
					return new List<Tuple<int, string>>();

               
				var statuses = lifecycleStatusRecords.Select(x => Mapper.Map<LifecycleStatusDto>(x));
				var nameList = new List<Tuple<int, string>>();
				nameList.AddRange(statuses.Select(x => new Tuple<int, string>(x.Id, x.Name)));
				return nameList.OrderBy(x => x.Item2);
			}
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var lifecycleStatus = context.LifecycleStatuses.Find(lifecycleStatusId);
				return Mapper.Map<LifecycleStatusDto>(lifecycleStatus);
			}
		}

		public ILifecycleStatusDto SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{

            //TODO: Brad - why is this here when the lifecycle controller initializes with a profile that has this line in it?
            //this doesn't even work anyways. 
            Mapper.Initialize(config => { config.CreateMap<LifecycleStatusDto, LifecycleStatus>(); }); //this is in the  profile
           

            using (var context = new PrometheusContext())
			{
				var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
				if (existingStatus == null)
				{
					var savedStatus = context.LifecycleStatuses.Add(Mapper.Map<LifecycleStatus>(lifecycleStatus));

					context.SaveChanges(_userId);
					return Mapper.Map<LifecycleStatusDto>(savedStatus);
				}
				else
				{
					return UpdateLifecycleStatus(lifecycleStatus);
				}
			}
		}

		private ILifecycleStatusDto UpdateLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			using (var context = new PrometheusContext())
			{
				var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
				if (existingStatus == null)
				{
					throw new InvalidOperationException("Serivce record must exist in order to be updated.");
				}
				else
				{
					var updatedStatus = Mapper.Map<LifecycleStatus>(lifecycleStatus);
					context.LifecycleStatuses.Attach(updatedStatus);
					context.Entry(updatedStatus).State = EntityState.Modified;
					context.SaveChanges(_userId);
					return Mapper.Map<LifecycleStatusDto>(updatedStatus);
				}
			}
		}

		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.LifecycleStatuses.Find(lifecycleStatusId);
				context.LifecycleStatuses.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return true;
		}
	}
}
