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
		/// <summary>
		/// Finds service process with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceProcessId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Modifies the service process in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing modification</param>
		/// <param name="serviceProcess"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Process</returns>
		public IServiceProcessDto ModifyServiceProcess(int performingUserId, IServiceProcessDto serviceProcess, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceProcess, modification);
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