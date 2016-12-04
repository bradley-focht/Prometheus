using Common.Enums;
using Common.Exceptions;

namespace ServicePortfolioService.Controllers
{
	public abstract class EntityController<T>
	{
		protected abstract T Create(T entity);
		protected abstract T Update(T entity);
		protected abstract T Delete(T entity);

		public T ModifyEntity(T entityDto, EntityModification modification)
		{
			switch (modification)
			{
				case EntityModification.Create:
					return Create(entityDto);
				case EntityModification.Update:
					return Update(entityDto);
				case EntityModification.Delete:
					return Delete(entityDto);
			}
			throw new ModificationException(string.Format("Modification {0} was not performed on entity {1}", modification, entityDto));
		}
	}
}
