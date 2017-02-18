using System;
using Common.Enums.Entities;
using Common.Exceptions;

namespace Common.Controllers
{
	public abstract class EntityController<T>
	{
		protected abstract T Create(T entity);
		protected abstract T Update(T entity);
		protected abstract T Delete(T entity);

		public T ModifyEntity(T entityDto, EntityModification modification)
		{
			if (entityDto == null)
				ThrowArgumentNullError(typeof(T).ToString());

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

		protected void ThrowArgumentNullError(string argumentName)
		{
			throw new ArgumentException($"Argument \"{argumentName}\" cannot be null", argumentName);
		}
	}
}
