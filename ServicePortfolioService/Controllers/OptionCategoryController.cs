using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
    public class OptionCategoryController : EntityController<IOptionCategoryDto>, IOptionCategoryController
    {
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public OptionCategoryController()
        {
            _userId = PortfolioService.GuestUserId;
        }

        public OptionCategoryController(int userId)
        {
            _userId = userId;
        }

        public IOptionCategoryDto GetOptionCategory(int optionCaegoryId)
        {
            using (var context = new PrometheusContext())
            {
                var serviceOption = context.OptionCategories.Find(optionCaegoryId);
                if (serviceOption != null)
                    return ManualMapper.MapOptionCategoryToDto(serviceOption);
                return null;
            }
        }

        public IOptionCategoryDto ModifyOptionCategory(IOptionCategoryDto optionCaegoryId, EntityModification modification)
        {
            return base.ModifyEntity(optionCaegoryId, modification);
        }

        protected override IOptionCategoryDto Create(IOptionCategoryDto entity)
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

        protected override IOptionCategoryDto Update(IOptionCategoryDto entity)
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

        protected override IOptionCategoryDto Delete(IOptionCategoryDto entity)
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