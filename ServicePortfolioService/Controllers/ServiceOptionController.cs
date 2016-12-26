using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
    public class ServiceOptionController : EntityController<IServiceOptionDto>, IServiceOptionController
    {
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public ServiceOptionController()
        {
            _userId = PortfolioService.GuestUserId;
        }

        public ServiceOptionController(int userId)
        {
            _userId = userId;
        }

        public IServiceOptionDto GetServiceOption(int serviceOptionId)
        {
            using (var context = new PrometheusContext())
            {
                var serviceOption = context.ServiceOptions.Find(serviceOptionId);
                if (serviceOption != null)
                    return ManualMapper.MapServiceOptionToDto(serviceOption);
                return null;
            }
        }

        public IServiceOptionDto ModifyServiceOption(IServiceOptionDto serviceOptionId, EntityModification modification)
        {
            return base.ModifyEntity(serviceOptionId, modification);
        }

        protected override IServiceOptionDto Create(IServiceOptionDto entity)
        {
            using (var context = new PrometheusContext())
            {
                var serviceOption = context.ServiceOptions.Find(entity.Id);
                if (serviceOption != null)
                {
                    throw new InvalidOperationException(string.Format("Service Option with ID {0} already exists.", entity.Id));
                }
                var savedOption = context.ServiceOptions.Add(ManualMapper.MapDtoToServiceOption(entity));
                context.SaveChanges(_userId);
                return ManualMapper.MapServiceOptionToDto(savedOption);
            }
        }

        protected override IServiceOptionDto Update(IServiceOptionDto entity)
        {
            using (var context = new PrometheusContext())
            {
                if (!context.ServiceOptions.Any(x => x.Id == entity.Id))
                {
                    throw new InvalidOperationException(string.Format("Service Option with ID {0} cannot be updated since it does not exist.", entity.Id));
                }
                var updatedOption = ManualMapper.MapDtoToServiceOption(entity);
                context.ServiceOptions.Attach(updatedOption);
                context.Entry(updatedOption).State = EntityState.Modified;
                context.SaveChanges(_userId);
                return ManualMapper.MapServiceOptionToDto(updatedOption);
            }
        }

        protected override IServiceOptionDto Delete(IServiceOptionDto entity)
        {
            using (var context = new PrometheusContext())
            {
                var toDelete = context.ServiceOptions.Find(entity.Id);
                context.ServiceOptions.Remove(toDelete);
                context.SaveChanges(_userId);
            }
            return null;
        }
    }
}