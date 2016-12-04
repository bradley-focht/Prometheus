using System;

namespace Common.Exceptions
{
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
