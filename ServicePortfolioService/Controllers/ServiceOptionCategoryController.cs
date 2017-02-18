using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class ServiceOptionCategoryController : EntityController<IServiceOptionCategoryDto>, IServiceOptionCategoryController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceOptionCategoryController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceOptionCategoryController(int userId)
		{
			_userId = userId;
		}

		public IServiceOptionCategoryDto GetServiceOptionCategory(int optionCaegoryId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.OptionCategories.Find(optionCaegoryId);
				if (serviceOption != null)
					return ManualMapper.MapOptionCategoryToDto(serviceOption);
				return null;
			}
		}

		public IServiceOptionCategoryDto ModifyServiceOptionCategory(IServiceOptionCategoryDto optionCaegoryId, EntityModification modification)
		{
			return base.ModifyEntity(optionCaegoryId, modification);
		}

		protected override IServiceOptionCategoryDto Create(IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.OptionCategories.Find(entity.Id);
				if (serviceOption != null)
				{
					throw new InvalidOperationException(string.Format("Service Option with ID {0} already exists.", entity.Id));
				}
				var savedOption = context.OptionCategories.Add(ManualMapper.MapDtoToOptionCategory(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapOptionCategoryToDto(savedOption);
			}
		}

		protected override IServiceOptionCategoryDto Update(IServiceOptionCategoryDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapOptionCategoryToDto(updatedOption);
			}
		}

		protected override IServiceOptionCategoryDto Delete(IServiceOptionCategoryDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.OptionCategories.Find(entity.Id);
				context.OptionCategories.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}