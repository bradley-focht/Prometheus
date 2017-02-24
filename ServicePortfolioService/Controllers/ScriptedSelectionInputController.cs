using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class ScriptedSelectionInputController : EntityController<IScriptedSelectionInputDto>, IScriptedSelectionController
	{
		public IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int selectionInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapScriptedSelectionInputToDto(context.ScriptedSelectionInputs.Find(selectionInputId));
			}
		}

		public IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs(int performingUserId)
		{
			using (var context = new PrometheusContext())
			{
				var inputs = context.ScriptedSelectionInputs;
				foreach (var input in inputs)
				{
					yield return ManualMapper.MapScriptedSelectionInputToDto(input);
				}
			}
		}

		public IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto textInput, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, textInput, modification);
		}

		protected override IScriptedSelectionInputDto Create(int performingUserId, IScriptedSelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var input = context.ScriptedSelectionInputs.Find(entity.Id);
				if (input != null)
				{
					throw new InvalidOperationException(string.Format("Scripted Selection with ID {0} already exists.", entity.Id));
				}
				var saveInput = context.ScriptedSelectionInputs.Add(ManualMapper.MapDtoToScriptedSelectionInput(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapScriptedSelectionInputToDto(saveInput);
			}
		}

		protected override IScriptedSelectionInputDto Update(int performingUserId, IScriptedSelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ScriptedSelectionInputs.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Measure with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedScriptedSelection = ManualMapper.MapDtoToScriptedSelectionInput(entity);
				context.ScriptedSelectionInputs.Attach(updatedScriptedSelection);
				context.Entry(updatedScriptedSelection).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapScriptedSelectionInputToDto(updatedScriptedSelection);
			}
		}

		protected override IScriptedSelectionInputDto Delete(int performingUserId, IScriptedSelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ScriptedSelectionInputs.Find(entity.Id);
				context.ScriptedSelectionInputs.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}