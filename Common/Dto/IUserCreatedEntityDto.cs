namespace Common.Dto
{
	/// <summary>
	/// Entity that can be created through user Action
	/// </summary>
	public interface IUserCreatedEntityDto : ICreatedEntityDto
	{
		/// <summary>
		/// ID of user that performed entity Creation
		/// </summary>
		int CreatedByUserId { get; set; }

		/// <summary>
		/// ID of User that performed last update to this record
		/// </summary>
		int UpdatedByUserId { get; set; }
	}
}
