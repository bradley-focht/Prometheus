using System;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class ServiceGoalController : EntityController<IServiceGoalDto>, IServiceGoalController
	{
		/// <summary>
		/// Finds service goal with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoalId"></param>
		/// <returns></returns>
		public IServiceGoalDto GetServiceGoal(int performingUserId, int serviceGoalId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceGoalToDto(context.ServiceGoals.Find(serviceGoalId));
			}
		}

		/// <summary>
		/// Modifies the service goal in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoal"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Goal</returns>
		public IServiceGoalDto ModifyServiceGoal(int performingUserId, IServiceGoalDto serviceGoal, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceGoal, modification);
		}

		protected override IServiceGoalDto Create(int performingUserId, IServiceGoalDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var goal = context.ServiceGoals.Find(entity.Id);
				if (goal != null)
				{
					throw new InvalidOperationException(string.Format("Service Goal with ID {0} already exists.", entity.Id));
				}
				var savedGoal = context.ServiceGoals.Add(ManualMapper.MapDtoToServiceGoal(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceGoalToDto(savedGoal);
			}
		}

		protected override IServiceGoalDto Update(int performingUserId, IServiceGoalDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceGoals.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Goal with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedServiceGoal = ManualMapper.MapDtoToServiceGoal(entity);
				context.ServiceGoals.Attach(updatedServiceGoal);
				context.Entry(updatedServiceGoal).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceGoalToDto(updatedServiceGoal);
			}
		}

		protected override IServiceGoalDto Delete(int performingUserId, IServiceGoalDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceGoals.Find(entity.Id);
				context.ServiceGoals.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}