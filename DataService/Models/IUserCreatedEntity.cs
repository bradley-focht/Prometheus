namespace DataService.Models
{
	/// <summary>
	/// Entity that can be created through user Action
	/// </summary>
	public interface IUserCreatedEntity : ICreatedEntity
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
