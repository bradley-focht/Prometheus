using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public class ServiceContractController : EntityController<IServiceContractDto>, IServiceContractController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceContractController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceContractController(int userId)
		{
			_userId = userId;
		}

		public IServiceContractDto GetServiceContract(int serviceContractId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapServiceContractToDto(context.ServiceContracts.Find(serviceContractId));
			}
		}

		public IServiceContractDto ModifyServiceContract(IServiceContractDto serviceContract, EntityModification modification)
		{
			return base.ModifyEntity(serviceContract, modification);
		}

		protected override IServiceContractDto Create(IServiceContractDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var contract = context.ServiceContracts.Find(entity.Id);
				if (contract != null)
				{
					throw new InvalidOperationException(string.Format("Service Contract with ID {0} already exists.", entity.Id));
				}
				var savedContract = context.ServiceContracts.Add(ManualMapper.MapDtoToServiceContract(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceContractToDto(savedContract);
			}
		}

		protected override IServiceContractDto Update(IServiceContractDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceContractToDto(updatedServiceContract);
			}
		}

		protected override IServiceContractDto Delete(IServiceContractDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceContracts.Find(entity.Id);
				context.ServiceContracts.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}