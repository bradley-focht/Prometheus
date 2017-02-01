using System;
using System.Collections.Generic;


namespace Common.Dto
{
	public class ServiceRequestPackageDto : IServiceRequestPackageDto
	{

		public int Id { get; set; }

		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<IServiceOptionCategoryDto> ServiceOptionCategories { get; set; }
	}
}
