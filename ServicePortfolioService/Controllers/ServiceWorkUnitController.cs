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
	public class ServiceWorkUnitController : EntityController<IServiceWorkUnitDto>, IServiceWorkUnitController
	{
		public IServiceWorkUnitDto GetServiceWorkUnit(int performingUserId, int serviceWorkUnitId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceWorkUnitToDto(context.ServiceWorkUnits.Find(serviceWorkUnitId));
			}
		}

		public IServiceWorkUnitDto ModifyServiceWorkUnit(int performingUserId, IServiceWorkUnitDto serviceWorkUnit, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceWorkUnit, modification);
		}

		protected override IServiceWorkUnitDto Create(int performingUserId, IServiceWorkUnitDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var workUnit = context.ServiceWorkUnits.Find(entity.Id);
				if (workUnit != null)
				{
					throw new InvalidOperationException(string.Format("Service WorkUnit with ID {0} already exists.", entity.Id));
				}
				var savedWorkUnit = context.ServiceWorkUnits.Add(ManualMapper.MapDtoToServiceWorkUnit(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceWorkUnitToDto(savedWorkUnit);
			}
		}

		protected override IServiceWorkUnitDto Update(int performingUserId, IServiceWorkUnitDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceWorkUnits.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service WorkUnit with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedServiceWorkUnit = ManualMapper.MapDtoToServiceWorkUnit(entity);
				context.ServiceWorkUnits.Attach(updatedServiceWorkUnit);
				context.Entry(updatedServiceWorkUnit).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceWorkUnitToDto(updatedServiceWorkUnit);
			}
		}

		protected override IServiceWorkUnitDto Delete(int performingUserId, IServiceWorkUnitDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceWorkUnits.Find(entity.Id);
				context.ServiceWorkUnits.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}