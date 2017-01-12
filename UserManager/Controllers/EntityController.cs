using Common.Enums.Entities;
using Common.Exceptions;

namespace UserManager.Controllers
{
	public abstract class EntityController<T>
	{
		protected abstract T Create(int performingUserId, T entity);
		protected abstract T Update(int performingUserId, T entity);
		protected abstract T Delete(int performingUserId, T entity);

		public T ModifyEntity(int performingUserId, T entityDto, EntityModification modification)
		{
			switch (modification)
			{
				case EntityModification.Create:
					return Create(performingUserId, entityDto);
				case EntityModification.Update:
					return Update(performingUserId, entityDto);
				case EntityModification.Delete:
					return Delete(performingUserId, entityDto);
			}
			throw new ModificationException(string.Format("Modification {0} was not performed on entity {1}", modification, entityDto));
		}
	}
}
