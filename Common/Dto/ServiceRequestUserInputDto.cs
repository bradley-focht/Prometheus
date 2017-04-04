using Common.Enums;

namespace Common.Dto
{
	/// <summary>
	/// The input value that a User supplies in response to a User Input that was requested in the Service Request process
	/// </summary>
	public class ServiceRequestUserInputDto : IServiceRequestUserInputDto
	{
		public int Id { get; set; }

		/// <summary>
		/// ID of SR that the Input is for
		/// </summary>
		public int ServiceRequestId { get; set; }

		/// <summary>
		/// ID of Input
		/// </summary>
		public int InputId { get; set; }

		/// <summary>
		/// Type of input on SR
		/// </summary>
		public UserInputType UserInputType { get; set; }

		/// <summary>
		/// Display name for input
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value the User supplied
		/// </summary>
		public string Value { get; set; }
	}
}