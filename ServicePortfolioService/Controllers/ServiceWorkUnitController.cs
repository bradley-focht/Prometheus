using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class ServiceWorkUnitController : EntityController<IServiceWorkUnitDto>, IServiceWorkUnitController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceWorkUnitController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceWorkUnitController(int userId)
		{
			_userId = userId;
		}

		public IServiceWorkUnitDto GetServiceWorkUnit(int serviceWorkUnitId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceWorkUnitToDto(context.ServiceWorkUnits.Find(serviceWorkUnitId));
			}
		}

		public IServiceWorkUnitDto ModifyServiceWorkUnit(IServiceWorkUnitDto serviceWorkUnit, EntityModification modification)
		{
			return base.ModifyEntity(serviceWorkUnit, modification);
		}

		protected override IServiceWorkUnitDto Create(IServiceWorkUnitDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var workUnit = context.ServiceWorkUnits.Find(entity.Id);
				if (workUnit != null)
				{
					throw new InvalidOperationException(string.Format("Service WorkUnit with ID {0} already exists.", entity.Id));
				}
				var savedWorkUnit = context.ServiceWorkUnits.Add(ManualMapper.MapDtoToServiceWorkUnit(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceWorkUnitToDto(savedWorkUnit);
			}
		}

		protected override IServiceWorkUnitDto Update(IServiceWorkUnitDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceWorkUnitToDto(updatedServiceWorkUnit);
			}
		}

		protected override IServiceWorkUnitDto Delete(IServiceWorkUnitDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceWorkUnits.Find(entity.Id);
				context.ServiceWorkUnits.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}