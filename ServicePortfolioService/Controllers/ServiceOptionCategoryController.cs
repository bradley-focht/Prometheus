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
	public class ServiceOptionCategoryController : EntityController<IServiceOptionCategoryDto>, IServiceOptionCategoryController
	{
		public IServiceOptionCategoryDto GetServiceOptionCategory(int performingUserId, int optionCaegoryId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.OptionCategories.Find(optionCaegoryId);
				if (serviceOption != null)
					return ManualMapper.MapOptionCategoryToDto(serviceOption);
				return null;
			}
		}

		public IServiceOptionCategoryDto ModifyServiceOptionCategory(int performingUserId, IServiceOptionCategoryDto optionCaegoryId, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, optionCaegoryId, modification);
		}

		protected override IServiceOptionCategoryDto Create(int performingUserId, IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.OptionCategories.Find(entity.Id);
				if (serviceOption != null)
				{
					throw new InvalidOperationException(string.Format("Service Option with ID {0} already exists.", entity.Id));
				}
				var savedOption = context.OptionCategories.Add(ManualMapper.MapDtoToOptionCategory(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapOptionCategoryToDto(savedOption);
			}
		}

		protected override IServiceOptionCategoryDto Update(int performingUserId, IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.OptionCategories.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(string.Format("Service Option with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedOption = ManualMapper.MapDtoToOptionCategory(entity);
				context.OptionCategories.Attach(updatedOption);
				context.Entry(updatedOption).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapOptionCategoryToDto(updatedOption);
			}
		}

		protected override IServiceOptionCategoryDto Delete(int performingUserId, IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.OptionCategories.Find(entity.Id);
				context.OptionCategories.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}