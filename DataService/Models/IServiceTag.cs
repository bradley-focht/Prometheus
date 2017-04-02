namespace DataService.Models
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Services
	/// </summary>
	public interface IServiceTag : IServicePackageTag
	{

		/// <summary>
		/// ID of Service linked
		/// </summary>
		int ServiceId { get; set; }
		Service Service { get; set; }
	}
}