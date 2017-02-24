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
	public class ServiceSwotController : EntityController<IServiceSwotDto>, IServiceSwotController
	{
		public IServiceSwotDto GetServiceSwot(int performingUserId, int serviceSwotId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceSwot = context.ServiceSwots.Find(serviceSwotId);
				if (serviceSwot != null)
					return ManualMapper.MapServiceSwotToDto(serviceSwot);
				return null;
			}
		}

		public IServiceSwotDto ModifyServiceSwot(int performingUserId, IServiceSwotDto serviceSwotId, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceSwotId, modification);
		}

		protected override IServiceSwotDto Create(int performingUserId, IServiceSwotDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceSwot = context.ServiceSwots.Find(entity.Id);
				if (serviceSwot != null)
				{
					throw new InvalidOperationException(string.Format("Service SWOT with ID {0} already exists.", entity.Id));
				}
				var savedSwot = context.ServiceSwots.Add(ManualMapper.MapDtoToServiceSwot(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceSwotToDto(savedSwot);
			}
		}

		protected override IServiceSwotDto Update(int performingUserId, IServiceSwotDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceSwots.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(string.Format("Service SWOT with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedSwot = ManualMapper.MapDtoToServiceSwot(entity);
				context.ServiceSwots.Attach(updatedSwot);
				context.Entry(updatedSwot).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceSwotToDto(updatedSwot);
			}
		}

		protected override IServiceSwotDto Delete(int performingUserId, IServiceSwotDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceSwots.Find(entity.Id);
				context.ServiceSwots.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}