namespace Common.Dto
{
	public interface ICatalogPublishable
	{
		int Id { get; set; }

		/// <summary>
		/// Used for sorting in the Service Catalog. Larger is more popular
		/// </summary>
		int Popularity { get; set; }

		/// <summary>
		/// Name displayed in the Service Catalog
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Service Catalog display information
		/// </summary>
		string BusinessValue { get; set; }

		/// <summary>
		/// Visible in Catalog
		/// </summary>
		bool Published { get; set; }
	}
}
