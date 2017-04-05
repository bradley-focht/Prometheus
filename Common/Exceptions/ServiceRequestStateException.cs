using System;

namespace Common.Exceptions
{
	/// <summary>
	/// Represents an error that took place where a Service Request was attempted to be placed into an illegal State.
	/// </summary>
	public class ServiceRequestStateException : Exception
	{
		public ServiceRequestStateException()
		{
		}

		public ServiceRequestStateException(string message) : base(message)
		{
		}

		public ServiceRequestStateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
