using Common.Enums;

namespace Common.Dto
{
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
		/// Value of the user input
		/// </summary>
		public string Value { get; set; }
	}
}