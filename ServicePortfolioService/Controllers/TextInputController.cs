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
	public class TextInputController : EntityController<ITextInputDto>, ITextInputController
	{
		public ITextInputDto GetTextInput(int performingUserId, int textInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapTextInputToDto(context.TextInputs.Find(textInputId));
			}
		}

		public IEnumerable<ITextInputDto> GetTextInputs(int performingUserId)
		{
			using (var context = new PrometheusContext())
			{
				var inputs = context.TextInputs;
				foreach (var input in inputs)
				{
					yield return ManualMapper.MapTextInputToDto(input);
				}
			}
		}

		public ITextInputDto ModifyTextInput(int performingUserId, ITextInputDto textInput, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, textInput, modification);
		}

		protected override ITextInputDto Create(int performingUserId, ITextInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.TextInputs.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.TextInputs.Add(ManualMapper.MapDtoToTextInput(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapTextInputToDto(savedMeasure);
			}
		}

		protected override ITextInputDto Update(int performingUserId, ITextInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.TextInputs.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Measure with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedTextInput = ManualMapper.MapDtoToTextInput(entity);
				context.TextInputs.Attach(updatedTextInput);
				context.Entry(updatedTextInput).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapTextInputToDto(updatedTextInput);
			}
		}

		protected override ITextInputDto Delete(int performingUserId, ITextInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.TextInputs.Find(entity.Id);
				context.TextInputs.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}