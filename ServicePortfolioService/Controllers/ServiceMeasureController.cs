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
	public class ServiceMeasureController : EntityController<IServiceMeasureDto>, IServiceMeasureController
	{
		/// <summary>
		/// Finds service Measure with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasureId"></param>
		/// <returns></returns>
		public IServiceMeasureDto GetServiceMeasure(int performingUserId, int serviceMeasureId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceMeasureToDto(context.ServiceMeasures.Find(serviceMeasureId));
			}
		}

		/// <summary>
		/// Modifies the service Measure in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasure"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		public IServiceMeasureDto ModifyServiceMeasure(int performingUserId, IServiceMeasureDto serviceMeasure, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceMeasure, modification);
		}

		protected override IServiceMeasureDto Create(int performingUserId, IServiceMeasureDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.ServiceMeasures.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.ServiceMeasures.Add(ManualMapper.MapDtoToServiceMeasure(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceMeasureToDto(savedMeasure);
			}
		}

		protected override IServiceMeasureDto Update(int performingUserId, IServiceMeasureDto entity)
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
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceMeasureToDto(updatedServiceMeasure);
			}
		}

		protected override IServiceMeasureDto Delete(int performingUserId, IServiceMeasureDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceMeasures.Find(entity.Id);
				context.ServiceMeasures.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}