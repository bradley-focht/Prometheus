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
	public class ScriptedSelectionInputController : EntityController<IScriptedSelectionInputDto>, IScriptedSelectionInputController
	{
		/// <summary>
		/// Finds Scripted Selection Input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <returns></returns>
		public IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int selectionInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapScriptedSelectionInputToDto(context.ScriptedSelectionInputs.Find(selectionInputId));
			}
		}

		/// <summary>
		/// Returns a list of all of the Scripted Selection Inputs found
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Modifies the Scripted Selection Input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Scripted Selection Input</returns>
		public IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto scriptedSelection, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, scriptedSelection, modification);
		}

		/// <summary>
		/// Creates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User creating the entity</param>
		/// <param name="entity">Entity to be created</param>
		/// <returns>Created entity DTO</returns>
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

		/// <summary>
		/// Updates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User updating the entity</param>
		/// <param name="entity">Entity to be updated</param>
		/// <returns>Updated entity DTO</returns>
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

		/// <summary>
		/// Deletes the entity in the database
		/// </summary>
		/// <param name="performingUserId">User deleting the entity</param>
		/// <param name="entity">Entity to be deleted</param>
		/// <returns>Deleted entity. null if deletion was successfull</returns>
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