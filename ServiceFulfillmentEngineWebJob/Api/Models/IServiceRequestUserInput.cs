using ServiceFulfillmentEngineWebJob.Api.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	/// <summary>
	/// The input value that a User supplies in response to a User Input that was requested in the Service Request process
	/// </summary>
	public interface IServiceRequestUserInput
	{
		int Id { get; set; }

		/// <summary>
		/// ID of SR that the Input is for
		/// </summary>
		int ServiceRequestId { get; set; }

		/// <summary>
		/// ID of Input
		/// </summary>
		int InputId { get; set; }

		/// <summary>
		/// Type of input on SR
		/// </summary>
		UserInputType UserInputType { get; set; }

		/// <summary>
		/// Display name for input
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Value the User supplied
		/// </summary>
		string Value { get; set; }
	}
}