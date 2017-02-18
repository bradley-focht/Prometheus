using Common.Dto;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
    public class ServiceProcessController : EntityController<IServiceProcessDto>, IServiceProcessController
    {
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public ServiceProcessController()
        {
            _userId = PortfolioService.GuestUserId;
        }

        public ServiceProcessController(int userId)
        {
            _userId = userId;
        }

        public IServiceProcessDto GetServiceProcess(int serviceProcessId)
        {
            using (var context = new PrometheusContext())
            {
                var serviceProcess = context.ServiceProcesses.Find(serviceProcessId);
                if (serviceProcess != null)
                    return ManualMapper.MapServiceProcessToDto(serviceProcess);
                return null;
            }
        }

        public IServiceProcessDto ModifyServiceProcess(IServiceProcessDto serviceProcessId, EntityModification modification)
        {
            return base.ModifyEntity(serviceProcessId, modification);
        }

        protected override IServiceProcessDto Create(IServiceProcessDto entity)
        {
            using (var context = new PrometheusContext())
            {
                var serviceProcess = context.ServiceProcesses.Find(entity.Id);
                if (serviceProcess != null)
                {
                    throw new InvalidOperationException(string.Format("Service Process with ID {0} already exists.", entity.Id));
                }
                var savedProcess = context.ServiceProcesses.Add(ManualMapper.MapDtoToServiceProcess(entity));
                context.SaveChanges(_userId);
                return ManualMapper.MapServiceProcessToDto(savedProcess);
            }
        }

        protected override IServiceProcessDto Update(IServiceProcessDto entity)
        {
            using (var context = new PrometheusContext())
            {
                if (!context.ServiceProcesses.Any(x => x.Id == entity.Id))
                {
                    throw new InvalidOperationException(string.Format("Service Process with ID {0} cannot be updated since it does not exist.", entity.Id));
                }
                var updatedProcess = ManualMapper.MapDtoToServiceProcess(entity);
                context.ServiceProcesses.Attach(updatedProcess);
                context.Entry(updatedProcess).State = EntityState.Modified;
                context.SaveChanges(_userId);
                return ManualMapper.MapServiceProcessToDto(updatedProcess);
            }
        }

        protected override IServiceProcessDto Delete(IServiceProcessDto entity)
        {
            using (var context = new PrometheusContext())
            {
                var toDelete = context.ServiceProcesses.Find(entity.Id);
                context.ServiceProcesses.Remove(toDelete);
                context.SaveChanges(_userId);
            }
            return null;
        }
    }
}