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
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceRequestOptionController(int userId)
		{
			_userId = userId;
		}

		public IServiceRequestOptionDto GetServiceRequestOption(int optionId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceRequestOptions.Find(optionId);
				if (serviceOption != null)
					return ManualMapper.MapServiceRequestOptionToDto(serviceOption);
				return null;
			}
		}

		public IServiceRequestOptionDto ModifyServiceRequestOption(IServiceRequestOptionDto requestOption, EntityModification modification)
		{
			return base.ModifyEntity(requestOption, modification);
		}

		protected override IServiceRequestOptionDto Create(IServiceRequestOptionDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceRequestOptions.Find(entity.Id);
				if (serviceOption != null)
				{
					throw new InvalidOperationException(string.Format("Service Request Option with ID {0} already exists.", entity.Id));
				}
				var savedOption = context.ServiceRequestOptions.Add(ManualMapper.MapDtoToServiceRequestOption(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceRequestOptionToDto(savedOption);
			}
		}

		protected override IServiceRequestOptionDto Update(IServiceRequestOptionDto entity)
		{
			throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Request Options. They can only be created and deleted.", EntityModification.Update));
		}

		protected override IServiceRequestOptionDto Delete(IServiceRequestOptionDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequestOptions.Find(entity.Id);
				context.ServiceRequestOptions.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}