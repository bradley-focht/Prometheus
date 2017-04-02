namespace Common.Dto
{
	/// <summary>
	/// ITIL Status that a Service can be in
	/// </summary>
	public interface ILifecycleStatusDto : IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// Attribute to decide if services with this status will be 
		/// visible in business / support catalog
		/// </summary>
		bool CatalogVisible { get; set; }

		/// <summary>
		/// Unique name of each status
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Used to order the display, this does not have any actual function other than display
		/// </summary>
		int Position { get; set; }
	}
}