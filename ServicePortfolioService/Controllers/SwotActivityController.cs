using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;

namespace ServicePortfolioService.Controllers
{
	public class SwotActivityController : EntityController<ISwotActivityDto>, ISwotActivityController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public SwotActivityController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public SwotActivityController(int userId)
		{
			_userId = userId;
		}

		public ISwotActivityDto GetSwotActivity(int swotActivityId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapSwotActivityToDto(context.SwotActivities.Find(swotActivityId));
			}
		}

		public ISwotActivityDto ModifySwotActivity(ISwotActivityDto swotActivity, EntityModification modification)
		{
			return base.ModifyEntity(swotActivity, modification);
		}

		protected override ISwotActivityDto Create(ISwotActivityDto activityDto)
		{
			using (var context = new PrometheusContext())
			{
				var activity = context.SwotActivities.Find(activityDto.Id);
				if (activity != null)
				{
					throw new InvalidOperationException(string.Format("SWOT Activity with ID {0} already exists.", activityDto.Id));
				}
				var savedActivity = context.SwotActivities.Add(ManualMapper.MapDtoToSwotActivity(activityDto));
				context.SaveChanges(_userId);
				return ManualMapper.MapSwotActivityToDto(savedActivity);
			}
		}

		protected override ISwotActivityDto Update(ISwotActivityDto activityDto)
		{
			using (var context = new PrometheusContext())
			{
				var updatedActivity = ManualMapper.MapDtoToSwotActivity(activityDto);
				if (updatedActivity == null)
				{
					throw new InvalidOperationException(string.Format("SWOT Activity with ID {0} cannot be updated since it does not exist.", activityDto.Id));
				}
				context.SwotActivities.Attach(updatedActivity);
				context.Entry(updatedActivity).State = EntityState.Modified;
				context.SaveChanges(_userId);
				return ManualMapper.MapSwotActivityToDto(updatedActivity);
			}
		}

		protected override ISwotActivityDto Delete(ISwotActivityDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.SwotActivities.Find(entity.Id);
				context.SwotActivities.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}