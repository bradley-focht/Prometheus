using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Dto;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class LifecycleStatusController : ILifecycleStatusController
	{
		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			using (var context = new PrometheusContext())
			{
				var lifecycleStatusRecords = context.LifecycleStatuses;

				//Empty list
				if (!lifecycleStatusRecords.Any())
					return new List<Tuple<int, string>>();

				var statuses = new List<LifecycleStatusDto>();
				foreach (var status in lifecycleStatusRecords)   //mapping and linq don't seem to get along
					statuses.Add(ManualMapper.MapLifecycleStatusToDto(status));

				var nameList = new List<Tuple<int, string>>();
				nameList.AddRange(statuses.OrderBy(x=>x.Position).Select(x => new Tuple<int, string>(x.Id, x.Name)));
				return nameList.OrderBy(x => x.Item2);
			}
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var lifecycleStatus = context.LifecycleStatuses.Find(lifecycleStatusId);
				//return Mapper.Map<LifecycleStatusDto>(lifecycleStatus);
				if (lifecycleStatus != null)
					return ManualMapper.MapLifecycleStatusToDto(lifecycleStatus);
				return null;
			}
		}

		public ILifecycleStatusDto SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			using (var context = new PrometheusContext())
			{
				var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
				if (existingStatus == null)
				{
					var savedStatus = context.LifecycleStatuses.Add(ManualMapper.MapDtoToLifecycleStatus(lifecycleStatus));
					context.SaveChanges();
					//return Mapper.Map<LifecycleStatusDto>(savedStatus);
					return ManualMapper.MapLifecycleStatusToDto(savedStatus);
				}
				else
				{
					return UpdateLifecycleStatus(lifecycleStatus);
				}
			}
		}

		//TODO: remove man mapper in here
		private ILifecycleStatusDto UpdateLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.LifecycleStatuses.Any(x => x.Id == lifecycleStatus.Id))
				{
					throw new InvalidOperationException("Lifecycle Status record must exist in order to be updated.");
				}
				var updatedStatus = ManualMapper.MapDtoToLifecycleStatus(lifecycleStatus);
				context.LifecycleStatuses.Attach(updatedStatus);
				context.Entry(updatedStatus).State = EntityState.Modified;
				context.SaveChanges();
				return ManualMapper.MapLifecycleStatusToDto(updatedStatus);
			}
		}

		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.LifecycleStatuses.Find(lifecycleStatusId);
				context.LifecycleStatuses.Remove(toDelete);
				context.SaveChanges();
			}
			return true;
		}

		public int CountLifecycleStatuses()
		{
			using (var context = new PrometheusContext())
			{
				return context.LifecycleStatuses.Count();
			}
		}
	}
}
