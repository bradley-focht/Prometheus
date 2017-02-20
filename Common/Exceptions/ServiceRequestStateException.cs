using System;

namespace Common.Exceptions
{
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
