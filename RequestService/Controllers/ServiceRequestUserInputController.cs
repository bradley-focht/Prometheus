using System;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	public class ServiceRequestUserInputController : EntityController<IServiceRequestUserInputDto>, IServiceRequestUserInputController
	{
		public IServiceRequestUserInputDto GetServiceRequestUserInput(int userInputId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequestUserInput = context.ServiceRequestUserInputs.Find(userInputId);
				if (serviceRequestUserInput != null)
					return ManualMapper.MapServiceRequestUserInputToDto(serviceRequestUserInput);
				return null;
			}
		}
		public IServiceRequestUserInputDto ModifyServiceRequestUserInput(IServiceRequestUserInputDto userInput, EntityModification modification)
		{
			return base.ModifyEntity(userInput, modification);
		}

		protected override IServiceRequestUserInputDto Create(IServiceRequestUserInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequestUserInput = context.ServiceRequestUserInputs.Find(entity.Id);
				if (serviceRequestUserInput != null)
				{
					throw new InvalidOperationException(string.Format("Service Request User Input with ID {0} already exists.", entity.Id));
				}
				var savedInput = context.ServiceRequestUserInputs.Add(ManualMapper.MapDtoToServiceRequestUserInput(entity));
				//TODO: ADD USER ID TO SAVECHANGES
				context.SaveChanges();
				return ManualMapper.MapServiceRequestUserInputToDto(savedInput);
			}
		}

		protected override IServiceRequestUserInputDto Update(IServiceRequestUserInputDto userInputDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceRequestUserInputs.Any(x => x.Id == userInputDto.Id))
				{
					throw new InvalidOperationException(string.Format("Service Request User Input with ID {0} cannot be updated since it does not exist.", userInputDto.Id));
				}
				var updated = ManualMapper.MapDtoToServiceRequestUserInput(userInputDto);
				context.ServiceRequestUserInputs.Attach(updated);
				context.Entry(updated).State = EntityState.Modified;
				//TODO: ADD USER ID TO SAVECHANGES
				context.SaveChanges();
				return ManualMapper.MapServiceRequestUserInputToDto(updated);
			}
		}

		protected override IServiceRequestUserInputDto Delete(IServiceRequestUserInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequestUserInputs.Find(entity.Id);
				context.ServiceRequestUserInputs.Remove(toDelete);
				//TODO: ADD USER ID TO SAVECHANGES
				context.SaveChanges();
			}
			return null;
		}
	}
}