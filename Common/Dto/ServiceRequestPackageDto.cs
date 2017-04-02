using System;
using System.Collections.Generic;
using Common.Enums;


namespace Common.Dto
{
	public class ServiceRequestPackageDto : IServiceRequestPackageDto
	{

		public int Id { get; set; }

		/// <summary>
		/// Action for SR to perform
		/// </summary>
		public ServiceRequestAction Action { get; set; }

		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<IServiceOptionCategoryTagDto> ServiceOptionCategoryTags { get; set; }
		public virtual ICollection<IServiceTagDto> ServiceTags { get; set; }
	}
}
