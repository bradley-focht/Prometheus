using System;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
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
			throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Option Categories.", EntityModification.Update));
		}

		protected override IServiceOptionCategoryDto Delete(int performingUserId, IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var tagsToDelete = context.ServiceOptionCategoryTags.Where(x => x.ServiceOptionCategoryId == entity.Id);
				context.ServiceOptionCategoryTags.RemoveRange(tagsToDelete);
				context.SaveChanges(performingUserId);

				var toDelete = context.OptionCategories.Find(entity.Id);
				context.OptionCategories.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}