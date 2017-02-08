using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServicePortfolioService.Controllers
{
	public class TextInputController : EntityController<ITextInputDto>, ITextInputController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public TextInputController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public TextInputController(int userId)
		{
			_userId = userId;
		}

		public ITextInputDto GetTextInput(int textInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapTextInputToDto(context.TextInputs.Find(textInputId));
			}
		}

		public IEnumerable<ITextInputDto> GetTextInputs()
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

		public ITextInputDto ModifyTextInput(ITextInputDto textInput, EntityModification modification)
		{
			return base.ModifyEntity(textInput, modification);
		}

		protected override ITextInputDto Create(ITextInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.TextInputs.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.TextInputs.Add(ManualMapper.MapDtoToTextInput(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapTextInputToDto(savedMeasure);
			}
		}

		protected override ITextInputDto Update(ITextInputDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapTextInputToDto(updatedTextInput);
			}
		}

		protected override ITextInputDto Delete(ITextInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.TextInputs.Find(entity.Id);
				context.TextInputs.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}