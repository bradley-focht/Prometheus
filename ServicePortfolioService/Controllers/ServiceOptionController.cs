using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
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

		public IInputGroupDto GetInputsForServiceOptions(IEnumerable<IServiceOptionDto> serviceOptions)
		{
			if (serviceOptions == null)
				base.ThrowArgumentNullError(nameof(serviceOptions));

			var inputGroup = new InputGroupDto();
			using (var context = new PrometheusContext())
			{
				var options = serviceOptions.Select(x => context.ServiceOptions.Find(x.Id));
				foreach (var option in options)
				{
					inputGroup.TextInputs.ToList().AddRange(option.TextInputs.Select(x => ManualMapper.MapTextInputToDto(x)));
					inputGroup.SelectionInputs.ToList().AddRange(option.SelectionInputs.Select(x => ManualMapper.MapSelectionInputToDto(x)));
					inputGroup.ScriptedSelectionInputs.ToList().AddRange(option.ScriptedSelectionInputs.Select(x => ManualMapper.MapScriptedSelectionInputToDto(x)));
				}
			}
			return inputGroup;
		}

		public IServiceOptionDto AddInputsToServiceOption(int serviceOptionId, IInputGroupDto inputsToAdd)
		{
			if (inputsToAdd == null)
				base.ThrowArgumentNullError(nameof(inputsToAdd));

			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceOptions.Find(serviceOptionId);
				if (serviceOption == null)
					throw new EntityNotFoundException("Could not add Inputs to Service Option.", typeof(ServiceOption), serviceOptionId);

				foreach (var textInputToAdd in inputsToAdd.TextInputs)
				{
					if (!serviceOption.TextInputs.Any(x => x.Id == textInputToAdd.Id))
						serviceOption.TextInputs.Add(context.TextInputs.Find(textInputToAdd.Id));
				}

				foreach (var selectionInputToAdd in inputsToAdd.SelectionInputs)
				{
					if (!serviceOption.SelectionInputs.Any(x => x.Id == selectionInputToAdd.Id))
						serviceOption.SelectionInputs.Add(context.SelectionInputs.Find(selectionInputToAdd.Id));
				}

				foreach (var scriptedSelectionInputToAdd in inputsToAdd.ScriptedSelectionInputs)
				{
					if (!serviceOption.ScriptedSelectionInputs.Any(x => x.Id == scriptedSelectionInputToAdd.Id))
						serviceOption.ScriptedSelectionInputs.Add(context.ScriptedSelectionInputs.Find(scriptedSelectionInputToAdd.Id));
				}

				context.SaveChanges(_userId);
				return ManualMapper.MapServiceOptionToDto(serviceOption);
			}
		}

		public IServiceOptionDto RemoveInputsFromServiceOption(int serviceOptionId, IInputGroupDto inputsToRemove)
		{
			if (inputsToRemove == null)
				base.ThrowArgumentNullError(nameof(inputsToRemove));

			using (var context = new PrometheusContext())
			{
				var serviceOption = context.ServiceOptions.Find(serviceOptionId);
				if (serviceOption == null)
					throw new EntityNotFoundException("Could not remove Inputs from Service Option.", typeof(ServiceOption), serviceOptionId);

				foreach (var textInputToRemove in inputsToRemove.TextInputs)
				{
					if (serviceOption.TextInputs.Any(x => x.Id == textInputToRemove.Id))
						serviceOption.TextInputs.Remove(context.TextInputs.Find(textInputToRemove.Id));
				}

				foreach (var selectionInputToRemove in inputsToRemove.SelectionInputs)
				{
					if (serviceOption.SelectionInputs.Any(x => x.Id == selectionInputToRemove.Id))
						serviceOption.SelectionInputs.Remove(context.SelectionInputs.Find(selectionInputToRemove.Id));
				}

				foreach (var scriptedSelectionInputToRemove in inputsToRemove.ScriptedSelectionInputs)
				{
					if (serviceOption.ScriptedSelectionInputs.Any(x => x.Id == scriptedSelectionInputToRemove.Id))
						serviceOption.ScriptedSelectionInputs.Remove(context.ScriptedSelectionInputs.Find(scriptedSelectionInputToRemove.Id));
				}

				context.SaveChanges(_userId);
				return ManualMapper.MapServiceOptionToDto(serviceOption);
			}
		}
	}
}