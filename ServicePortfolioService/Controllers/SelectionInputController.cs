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
	public class SelectionInputController : EntityController<ISelectionInputDto>, ISelectionInputController
	{
		public ISelectionInputDto GetSelectionInput(int performingUserId, int textInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapSelectionInputToDto(context.SelectionInputs.Find(textInputId));
			}
		}

	    public IEnumerable<ISelectionInputDto> GetSelectionInputs(int performingUserId)
	    {
	        using (var context = new PrometheusContext())
	        {
	            var inputs = context.SelectionInputs;
	            foreach (var input in inputs)
	            {
	                yield return ManualMapper.MapSelectionInputToDto(input);
	            }
	        }
	    }

	    public ISelectionInputDto ModifySelectionInput(int performingUserId, ISelectionInputDto textInput, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, textInput, modification);
		}

		protected override ISelectionInputDto Create(int performingUserId, ISelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.SelectionInputs.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.SelectionInputs.Add(ManualMapper.MapDtoToSelectionInput(entity));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapSelectionInputToDto(savedMeasure);
			}
		}

		protected override ISelectionInputDto Update(int performingUserId, ISelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.SelectionInputs.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Measure with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedSelectionInput = ManualMapper.MapDtoToSelectionInput(entity);
				context.SelectionInputs.Attach(updatedSelectionInput);
				context.Entry(updatedSelectionInput).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapSelectionInputToDto(updatedSelectionInput);
			}
		}

		protected override ISelectionInputDto Delete(int performingUserId, ISelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.SelectionInputs.Find(entity.Id);
				context.SelectionInputs.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}