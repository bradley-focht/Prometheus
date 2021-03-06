﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Services
	/// </summary>
	public class ServiceTag : IServiceTag
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

		public virtual Service Service { get; set; }
		public virtual ServiceRequestPackage ServiceRequestPackage { get; set; }
	}
}