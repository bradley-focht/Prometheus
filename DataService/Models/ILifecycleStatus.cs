﻿namespace DataService.Models
{
	public interface ILifecycleStatus : IUserCreatedEntity
	{
		bool CatalogVisible { get; set; }
		string Comment { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		int Position { get; set; }
		//int ServiceId { get; set; }
		Service Service { get; set; }
	}
}