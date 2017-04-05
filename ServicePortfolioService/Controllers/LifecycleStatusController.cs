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
		/// <summary>
		/// KVP of all lifecycle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Finds lifecycle status with identifier provided and returns its DTO
		/// </summary>
		/// <param name="lifecycleStatusId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// returns a count of the number of Lifecycle statuses found
		/// </summary>
		/// <returns></returns>
		public int CountLifecycleStatuses()
		{
			using (var context = new PrometheusContext())
			{
				return context.LifecycleStatuses.Count();
			}
		}

		/// <summary>
		/// Modifies the status in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing the modification</param>
		/// <param name="status"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Lifecycle Status</returns>
		public ILifecycleStatusDto ModifyLifecycleStatus(int performingUserId, ILifecycleStatusDto status,
			EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, status, modification);
		}

		/// <summary>
		/// Creates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User creating the entity</param>
		/// <param name="entity">Entity to be created</param>
		/// <returns>Created entity DTO</returns>
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

		/// <summary>
		/// Updates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User updating the entity</param>
		/// <param name="entity">Entity to be updated</param>
		/// <returns>Updated entity DTO</returns>
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
					//Update positions to allow for position change backwards
					if (newPosition < currentPosition && status.Position >= newPosition && status.Position < currentPosition)
					{
						status.Position++;
						context.LifecycleStatuses.Attach(status);
						context.Entry(status).State = EntityState.Modified;
					}

					//Update positions to allow for position change forwards
					if (newPosition > currentPosition && status.Position <= newPosition && status.Position > currentPosition)
					{
						status.Position--;
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

		/// <summary>
		/// Deletes the entity in the database
		/// </summary>
		/// <param name="performingUserId">User deleting the entity</param>
		/// <param name="entity">Entity to be deleted</param>
		/// <returns>Deleted entity. null if deletion was successfull</returns>
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
