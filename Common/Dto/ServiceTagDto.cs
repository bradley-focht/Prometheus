using System;

namespace Common.Dto
{
	public class ServiceTagDto : IServiceTagDto
	{
		public int Id { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public int Order { get; set; }
		public int ServiceId { get; set; }
		public int ServiceRequestPackageId { get; set; }

		public IServiceDto Service { get; set; }
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}