using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class ServiceSwotController : EntityController<IServiceSwotDto>, IServiceSwotController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceSwotController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceSwotController(int userId)
		{
			_userId = userId;
		}

		public IServiceSwotDto GetServiceSwot(int serviceSwotId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceSwot = context.ServiceSwots.Find(serviceSwotId);
				if (serviceSwot != null)
					return ManualMapper.MapServiceSwotToDto(serviceSwot);
				return null;
			}
		}

		public IServiceSwotDto ModifyServiceSwot(IServiceSwotDto serviceSwotId, EntityModification modification)
		{
			return base.ModifyEntity(serviceSwotId, modification);
		}

		protected override IServiceSwotDto Create(IServiceSwotDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceSwot = context.ServiceSwots.Find(entity.Id);
				if (serviceSwot != null)
				{
					throw new InvalidOperationException(string.Format("Service SWOT with ID {0} already exists.", entity.Id));
				}
				var savedSwot = context.ServiceSwots.Add(ManualMapper.MapDtoToServiceSwot(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceSwotToDto(savedSwot);
			}
		}

		protected override IServiceSwotDto Update(IServiceSwotDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceSwotToDto(updatedSwot);
			}
		}

		protected override IServiceSwotDto Delete(IServiceSwotDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceSwots.Find(entity.Id);
				context.ServiceSwots.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}