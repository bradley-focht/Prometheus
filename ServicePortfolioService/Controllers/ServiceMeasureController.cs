using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class ServiceMeasureController : EntityController<IServiceMeasureDto>, IServiceMeasureController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceMeasureController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceMeasureController(int userId)
		{
			_userId = userId;
		}

		public IServiceMeasureDto GetServiceMeasure(int serviceMeasureId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceMeasureToDto(context.ServiceMeasures.Find(serviceMeasureId));
			}
		}

		public IServiceMeasureDto ModifyServiceMeasure(IServiceMeasureDto serviceMeasure, EntityModification modification)
		{
			return base.ModifyEntity(serviceMeasure, modification);
		}

		protected override IServiceMeasureDto Create(IServiceMeasureDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.ServiceMeasures.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.ServiceMeasures.Add(ManualMapper.MapDtoToServiceMeasure(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceMeasureToDto(savedMeasure);
			}
		}

		protected override IServiceMeasureDto Update(IServiceMeasureDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceMeasures.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Measure with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedServiceMeasure = ManualMapper.MapDtoToServiceMeasure(entity);
				context.ServiceMeasures.Attach(updatedServiceMeasure);
				context.Entry(updatedServiceMeasure).State = EntityState.Modified;
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceMeasureToDto(updatedServiceMeasure);
			}
		}

		protected override IServiceMeasureDto Delete(IServiceMeasureDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceMeasures.Find(entity.Id);
				context.ServiceMeasures.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}