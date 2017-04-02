using System;

namespace Common.Dto
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Services
	/// </summary>
	public class ServiceTagDto : IServiceTagDto
	{
		public int Id { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Place in the order of Tags on a Service Package
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// ID of Service linked
		/// </summary>
		public int ServiceId { get; set; }

		/// <summary>
		/// ID of Service Package linked
		/// </summary>
		public int ServiceRequestPackageId { get; set; }

		public IServiceDto Service { get; set; }
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}