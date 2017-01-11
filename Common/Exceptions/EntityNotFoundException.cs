using System;

namespace Common.Exceptions
{
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

		private static string ModifyMessage(string message, Type entity, int id)
		{
			return string.Format("{0} with ID {1} not found. {2}", entity, id, message);
		}
	}
}
