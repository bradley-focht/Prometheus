using System;
using Common.Enums.Entities;
using Common.Exceptions;

namespace Common.Controllers
{
	public abstract class EntityController<T>
	{
		/// <summary>
		/// Creates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User creating the entity</param>
		/// <param name="entity">Entity to be created</param>
		/// <returns>Created entity DTO</returns>
		protected abstract T Create(int performingUserId, T entity);

		/// <summary>
		/// Updates the entity in the database
		/// </summary>
		/// <param name="performingUserId">User updating the entity</param>
		/// <param name="entity">Entity to be updated</param>
		/// <returns>Updated entity DTO</returns>
		protected abstract T Update(int performingUserId, T entity);

		/// <summary>
		/// Deletes the entity in the database
		/// </summary>
		/// <param name="performingUserId">User deleting the entity</param>
		/// <param name="entity">Entity to be deleted</param>
		/// <returns>Deleted entity. null if deletion was successfull</returns>
		protected abstract T Delete(int performingUserId, T entity);

		/// <summary>
		/// Checks to determine if the user has the required permission in order to perform the entity modification.
		/// 
		/// NOTE: Override method to apply a permission.
		/// </summary>
		/// <param name="performingUserId">ID for user performing the modification</param>
		/// <param name="entityDto">Entity attempted to be modified</param>
		/// <param name="modification">modification being attempted</param>
		/// <param name="permission">Permission that is required to perform the modification. Object passed in does not matter.</param>
		/// <returns>If the user can perform the modification</returns>
		protected virtual bool UserHasPermissionToModify(int performingUserId, T entityDto, EntityModification modification, out object permission)
		{
			permission = null;
			return true;
		}

		/// <summary>
		/// Determines if the user has permission to modify an entity, then performs that modification to the database.
		/// </summary>
		/// <param name="performingUserId">User performing the modification</param>
		/// <param name="entityDto">entity to be modified</param>
		/// <param name="modification">type of modification</param>
		/// <returns>The modified entity DTO</returns>
		protected T ModifyEntity(int performingUserId, T entityDto, EntityModification modification)
		{
			if (entityDto == null)
				ThrowArgumentNullError(typeof(T).ToString());

			object permission;
			if (UserHasPermissionToModify(performingUserId, entityDto, modification, out permission))
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
				throw new ModificationException(string.Format("Modification {0} was not performed on entity {1}", modification,
					entityDto));
			}
			throw new PermissionException(string.Format("No permission to modify entity {0}", typeof(T)), performingUserId, permission);
		}

		protected void ThrowArgumentNullError(string argumentName)
		{
			throw new ArgumentException($"Argument \"{argumentName}\" cannot be null", argumentName);
		}
	}
}
