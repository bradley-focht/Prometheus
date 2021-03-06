﻿namespace DataService.Models
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and entities in Service Packages
	/// </summary>
	public interface IServicePackageTag : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Place in the order of Tags on a Service Package
		/// </summary>
		int Order { get; set; }

		/// <summary>
		/// ID of Service Package linked
		/// </summary>
		int ServiceRequestPackageId { get; set; }
		ServiceRequestPackage ServiceRequestPackage { get; set; }
	}
}