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
	public class LifecycleStatusController : EntityController<ILifecycleStatusDto>, ILifecycleStatusController
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
				nameList.AddRange(statuses.OrderBy(x => x.Position).Select(x => new Tuple<int, string>(x.Id, x.Name)));
				return nameList.OrderBy(x => x.Item1);
			}
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var lifecycleStatus = context.LifecycleStatuses.Find(lifecycleStatusId);
				if (lifecycleStatus != null)
					return ManualMapper.MapLifecycleStatusToDto(lifecycleStatus);
				return null;
			}
		}

		public int CountLifecycleStatuses()
		{
			using (var context = new PrometheusContext())
			{
				return context.LifecycleStatuses.Count();
			}
		}

		public ILifecycleStatusDto ModifyLifecycleStatus(int performingUserId, ILifecycleStatusDto status,
			EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, status, modification);
		}

		protected override ILifecycleStatusDto Create(int performingUserId, ILifecycleStatusDto lifecycleStatus)
		{
			using (var context = new PrometheusContext())
			{
				var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
				if (existingStatus == null)
				{
					//Insert at correct Position
					foreach (var status in context.LifecycleStatuses)
					{
						if (status.Position >= lifecycleStatus.Position)
						{
							status.Position++;
							context.LifecycleStatuses.Attach(status);
							context.Entry(status).State = EntityState.Modified;
						}
					}

					var savedStatus = context.LifecycleStatuses.Add(ManualMapper.MapDtoToLifecycleStatus(lifecycleStatus));
					context.SaveChanges(performingUserId);
					return ManualMapper.MapLifecycleStatusToDto(savedStatus);
				}
				else
				{
					throw new InvalidOperationException(string.Format("Lifecycle Status with ID {0} already exists.", lifecycleStatus.Id));
				}
			}
		}

		protected override ILifecycleStatusDto Update(int performingUserId, ILifecycleStatusDto lifecycleStatusDto)
		{
			using (var context = new PrometheusContext())
			{
				var statusEntity = context.LifecycleStatuses.FirstOrDefault(x => x.Id == lifecycleStatusDto.Id);
				if (statusEntity == null)
				{
					throw new InvalidOperationException("Lifecycle Status record must exist in order to be updated.");
				}

				var currentPosition = statusEntity.Position;
				var newPosition = lifecycleStatusDto.Position;

				foreach (var status in context.LifecycleStatuses)
				{
					//Update positions to allow for position change
					if (status.Position >= newPosition && status.Position < currentPosition)
					{
						status.Position++;
						context.LifecycleStatuses.Attach(status);
						context.Entry(status).State = EntityState.Modified;
					}
				}

				var updatedStatus = ManualMapper.MapDtoToLifecycleStatus(lifecycleStatusDto);
				context.LifecycleStatuses.Attach(updatedStatus);
				context.Entry(updatedStatus).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapLifecycleStatusToDto(updatedStatus);
			}
		}

		protected override ILifecycleStatusDto Delete(int performingUserId, ILifecycleStatusDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.LifecycleStatuses.Find(entity.Id);
				context.LifecycleStatuses.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}
