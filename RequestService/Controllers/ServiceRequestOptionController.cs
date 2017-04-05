using System;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	public class ServiceRequestOptionController : EntityController<IServiceRequestOptionDto>, IServiceRequestOptionController
	{
		/// <summary>
		/// Finds service request option with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="optionId"></param>
		/// <returns></returns>
		public IServiceRequestOptionDto GetServiceRequestOption(int performingUserId, int optionId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceRequestOptions.Find(optionId);
				if (serviceOption != null)
					return ManualMapper.MapServiceRequestOptionToDto(serviceOption);
				return null;
			}
		}

		/// <summary>
		/// Modifies the service request option in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="requestOption"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request Option</returns>
		public IServiceRequestOptionDto ModifyServiceRequestOption(int performingUserId, IServiceRequestOptionDto requestOption, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, requestOption, modification);
		}

		protected override IServiceRequestOptionDto Create(int performingUserId, IServiceRequestOptionDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceRequestOptions.Find(entity.Id);
				if (serviceOption != null)
				{
					throw new InvalidOperationException(string.Format("Service Request Option with ID {0} already exists.", entity.Id));
				}
				var savedOption = context.ServiceRequestOptions.Add(ManualMapper.MapDtoToServiceRequestOption(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceRequestOptionToDto(savedOption);
			}
		}

		protected override IServiceRequestOptionDto Update(int performingUserId, IServiceRequestOptionDto entity)
		{
			throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Request Options. They can only be created and deleted.", EntityModification.Update));
		}

		protected override IServiceRequestOptionDto Delete(int performingUserId, IServiceRequestOptionDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequestOptions.Find(entity.Id);
				context.ServiceRequestOptions.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}