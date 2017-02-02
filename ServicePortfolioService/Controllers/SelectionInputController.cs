using Common.Dto;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public class SelectionInputController : EntityController<ISelectionInputDto>, ISelectionInputController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public SelectionInputController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public SelectionInputController(int userId)
		{
			_userId = userId;
		}

		public ISelectionInputDto GetSelectionInput(int textInputId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapSelectionInputToDto(context.SelectionInputs.Find(textInputId));
			}
		}

	    public IEnumerable<ISelectionInputDto> GetSelectionInputs()
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

	    public ISelectionInputDto ModifySelectionInput(ISelectionInputDto textInput, EntityModification modification)
		{
			return base.ModifyEntity(textInput, modification);
		}

		protected override ISelectionInputDto Create(ISelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var measure = context.SelectionInputs.Find(entity.Id);
				if (measure != null)
				{
					throw new InvalidOperationException(string.Format("Service Measure with ID {0} already exists.", entity.Id));
				}
				var savedMeasure = context.SelectionInputs.Add(ManualMapper.MapDtoToSelectionInput(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapSelectionInputToDto(savedMeasure);
			}
		}

		protected override ISelectionInputDto Update(ISelectionInputDto entity)
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
				context.SaveChanges(_userId);
				return ManualMapper.MapSelectionInputToDto(updatedSelectionInput);
			}
		}

		protected override ISelectionInputDto Delete(ISelectionInputDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.SelectionInputs.Find(entity.Id);
				context.SelectionInputs.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}