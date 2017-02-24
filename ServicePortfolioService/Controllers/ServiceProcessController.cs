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
	public class ServiceProcessController : EntityController<IServiceProcessDto>, IServiceProcessController
	{
		public IServiceProcessDto GetServiceProcess(int performingUserId, int serviceProcessId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceProcess = context.ServiceProcesses.Find(serviceProcessId);
				if (serviceProcess != null)
					return ManualMapper.MapServiceProcessToDto(serviceProcess);
				return null;
			}
		}

		public IServiceProcessDto ModifyServiceProcess(int performingUserId, IServiceProcessDto serviceProcessId, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceProcessId, modification);
		}

		protected override IServiceProcessDto Create(int performingUserId, IServiceProcessDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceProcess = context.ServiceProcesses.Find(entity.Id);
				if (serviceProcess != null)
				{
					throw new InvalidOperationException(string.Format("Service Process with ID {0} already exists.", entity.Id));
				}
				var savedProcess = context.ServiceProcesses.Add(ManualMapper.MapDtoToServiceProcess(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceProcessToDto(savedProcess);
			}
		}

		protected override IServiceProcessDto Update(int performingUserId, IServiceProcessDto entity)
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
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceProcessToDto(updatedProcess);
			}
		}

		protected override IServiceProcessDto Delete(int performingUserId, IServiceProcessDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceProcesses.Find(entity.Id);
				context.ServiceProcesses.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}