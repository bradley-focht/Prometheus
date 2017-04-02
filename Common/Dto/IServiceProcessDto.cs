namespace Common.Dto
{
	public interface IServiceProcessDto : ICreatedEntityDto
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		/// <summary>
		/// Process name, should be unique in the service
		/// </summary>
		string Name { get; set; }
		string Owner { get; set; }
		string Description { get; set; }

		/// <summary>
		/// key business benefits
		/// </summary>
		string Benefits { get; set; }

		/// <summary>
		/// Area of continuous improvement
		/// </summary>
		string Improvements { get; set; }

	}
}
