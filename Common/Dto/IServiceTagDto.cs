namespace Common.Dto
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Services
	/// </summary>
	public interface IServiceTagDto : IServicePackageTag
	{
		/// <summary>
		/// ID of Service linked
		/// </summary>
		int ServiceId { get; set; }
		IServiceDto Service { get; set; }
	}
}
