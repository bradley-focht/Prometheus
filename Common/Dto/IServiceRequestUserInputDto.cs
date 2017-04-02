using Common.Enums;

namespace Common.Dto
{
	public interface IServiceRequestUserInputDto
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
		/// Value of the user input
		/// </summary>
		string Value { get; set; }
	}
}
