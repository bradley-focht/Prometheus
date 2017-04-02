using System;

namespace Common.Dto
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Service Option Categories
	/// </summary>
	public class ServiceOptionCategoryTagDto : IServiceOptionCategoryTagDto
	{
		public int Id { get; set; }

		/// <summary>
		/// Place in the order of Tags on a Service Package
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// ID of Service Option Category linked
		/// </summary>
		public int ServiceOptionCategoryId { get; set; }

		/// <summary>
		/// ID of Service Package linked
		/// </summary>
		public int ServiceRequestPackageId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}
