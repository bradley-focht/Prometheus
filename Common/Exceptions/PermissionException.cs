using System;

namespace Common.Exceptions
{
	/// <summary>
	/// Permission to be used when a user attempts an action they do not have permission for.
	/// </summary>
	public class PermissionException : Exception
	{
		public PermissionException()
		{
		}

		public PermissionException(string message) : base(message)
		{
		}

		public PermissionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public PermissionException(string message, int userId, object permission) : base(ModifyMessage(message, userId, permission))
		{

		}

		private static string ModifyMessage(string message, int userId, object permission)
		{
			return string.Format("User with ID {0} does not have permission {1}. {2}", userId, permission, message);
		}
	}
}
