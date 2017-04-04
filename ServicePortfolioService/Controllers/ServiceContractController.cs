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
	public class ServiceContractController : EntityController<IServiceContractDto>, IServiceContractController
	{
		/// <summary>
		/// Finds service Contract with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContractId"></param>
		/// <returns></returns>
		public IServiceContractDto GetServiceContract(int performingUserId, int serviceContractId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceContractToDto(context.ServiceContracts.Find(serviceContractId));
			}
		}

		/// <summary>
		/// Modifies the service Contract in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContract"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Contract</returns>
		public IServiceContractDto ModifyServiceContract(int performingUserId, IServiceContractDto serviceContract, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceContract, modification);
		}

		protected override IServiceContractDto Create(int performingUserId, IServiceContractDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var contract = context.ServiceContracts.Find(entity.Id);
				if (contract != null)
				{
					throw new InvalidOperationException(string.Format("Service Contract with ID {0} already exists.", entity.Id));
				}
				var savedContract = context.ServiceContracts.Add(ManualMapper.MapDtoToServiceContract(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceContractToDto(savedContract);
			}
		}

		protected override IServiceContractDto Update(int performingUserId, IServiceContractDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceContracts.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Contract with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedServiceContract = ManualMapper.MapDtoToServiceContract(entity);
				context.ServiceContracts.Attach(updatedServiceContract);
				context.Entry(updatedServiceContract).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceContractToDto(updatedServiceContract);
			}
		}

		protected override IServiceContractDto Delete(int performingUserId, IServiceContractDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceContracts.Find(entity.Id);
				context.ServiceContracts.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}