using System;

namespace Common.Exceptions
{
	/// <summary>
	/// Represents an error that took place where an Entity in Prometheus was requested or required to perform an action
	/// and that Entity could not be located in the Database.
	/// </summary>
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
		{
		}

		public EntityNotFoundException(string message) : base(message)
		{
		}

		public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public EntityNotFoundException(string message, Type entity, int id) : base(ModifyMessage(message, entity, id))
		{

		}

		/// <summary>
		/// Builds the message for an EntityNotFoundException
		/// </summary>
		/// <param name="message"></param>
		/// <param name="entity">Entity ID was searched for</param>
		/// <param name="id">ID not found for the entity searched for</param>
		/// <returns></returns>
		private static string ModifyMessage(string message, Type entity, int id)
		{
			return string.Format("{0} with ID {1} not found. {2}", entity, id, message);
		}
	}
}
