namespace Common.Dto
{
	public interface IServiceMeasureDto : IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// ID of the Service that the Measure applies to 
		/// </summary>
		int ServiceId { get; set; }

		/// <summary>
		/// Measurement method used, such as survey
		/// </summary>
		string Method { get; set; }

		/// <summary>
		/// Results of the measurement method used
		/// </summary>
		string Outcome { get; set; }
	}
}