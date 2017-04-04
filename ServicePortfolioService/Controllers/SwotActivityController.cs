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
	public class SwotActivityController : EntityController<ISwotActivityDto>, ISwotActivityController
	{
		/// <summary>
		/// Finds SWOT activity with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivityId"></param>
		/// <returns></returns>
		public ISwotActivityDto GetSwotActivity(int performingUserId, int swotActivityId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapSwotActivityToDto(context.SwotActivities.Find(swotActivityId));
			}
		}

		/// <summary>
		/// Modifies the SWOT activity in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivity"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT activity</returns>
		public ISwotActivityDto ModifySwotActivity(int performingUserId, ISwotActivityDto swotActivity, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, swotActivity, modification);
		}

		protected override ISwotActivityDto Create(int performingUserId, ISwotActivityDto activityDto)
		{
			using (var context = new PrometheusContext())
			{
				var activity = context.SwotActivities.Find(activityDto.Id);
				if (activity != null)
				{
					throw new InvalidOperationException(string.Format("SWOT Activity with ID {0} already exists.", activityDto.Id));
				}
				var savedActivity = context.SwotActivities.Add(ManualMapper.MapDtoToSwotActivity(activityDto));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapSwotActivityToDto(savedActivity);
			}
		}

		protected override ISwotActivityDto Update(int performingUserId, ISwotActivityDto activityDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.SwotActivities.Any(x => x.Id == activityDto.Id))
				{
					throw new InvalidOperationException(string.Format("SWOT Activity with ID {0} cannot be updated since it does not exist.", activityDto.Id));
				}
				var updatedActivity = ManualMapper.MapDtoToSwotActivity(activityDto);
				context.SwotActivities.Attach(updatedActivity);
				context.Entry(updatedActivity).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapSwotActivityToDto(updatedActivity);
			}
		}

		protected override ISwotActivityDto Delete(int performingUserId, ISwotActivityDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.SwotActivities.Find(entity.Id);
				context.SwotActivities.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}