﻿using System;

namespace Common.Dto
{
	public class ServiceOptionCategoryTagDto : IServiceOptionCategoryTagDto
	{
		public int Id { get; set; }

		public int Order { get; set; }
		public int ServiceOptionCategoryId { get; set; }
		public int ServiceRequestPackageId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}
