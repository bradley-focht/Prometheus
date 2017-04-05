using System;

namespace Common.Exceptions
{
	/// <summary>
	/// Represents an error that took place where an Entity modification in Prometheus was requested that is either disallowed or
	/// could not be executed.
	/// </summary>
	public class ModificationException : Exception
	{
		public ModificationException()
		{
		}

		public ModificationException(string message) : base(message)
		{
		}

		public ModificationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
