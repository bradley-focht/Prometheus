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
		/// <summary>
		/// Finds service request user input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="userInputId"></param>
		/// <returns></returns>
		public IServiceRequestUserInputDto GetServiceRequestUserInput(int performingUserId, int userInputId)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequestUserInput = context.ServiceRequestUserInputs.Find(userInputId);
				if (serviceRequestUserInput != null)
					return ManualMapper.MapServiceRequestUserInputToDto(serviceRequestUserInput);
				return null;
			}
		}

		/// <summary>
		/// Modifies the service request input in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing the modification</param>
		/// <param name="userInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request User Input</returns>
		public IServiceRequestUserInputDto ModifyServiceRequestUserInput(int performingUserId, IServiceRequestUserInputDto userInput, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, userInput, modification);
		}

		protected override IServiceRequestUserInputDto Create(int performingUser, IServiceRequestUserInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var serviceRequestUserInput = context.ServiceRequestUserInputs.Find(entity.Id);
				if (serviceRequestUserInput != null)
				{
					throw new InvalidOperationException(string.Format("Service Request User Input with ID {0} already exists.", entity.Id));
				}
				var savedInput = context.ServiceRequestUserInputs.Add(ManualMapper.MapDtoToServiceRequestUserInput(entity));
				context.SaveChanges(performingUser);
				return ManualMapper.MapServiceRequestUserInputToDto(savedInput);
			}
		}

		protected override IServiceRequestUserInputDto Update(int performingUser, IServiceRequestUserInputDto userInputDto)
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
				context.SaveChanges(performingUser);
				return ManualMapper.MapServiceRequestUserInputToDto(updated);
			}
		}

		protected override IServiceRequestUserInputDto Delete(int performingUser, IServiceRequestUserInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequestUserInputs.Find(entity.Id);
				context.ServiceRequestUserInputs.Remove(toDelete);
				context.SaveChanges(performingUser);
			}
			return null;
		}

		protected override bool UserHasPermissionToModify(int performingUserId, IServiceRequestUserInputDto requestUserInput, EntityModification modification, out object permission)
		{
			permission = null;
			return true;
		}
	}
}