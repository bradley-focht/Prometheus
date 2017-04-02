using System;

namespace Common.Dto
{
	public interface ISwotActivityDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceSwotId { get; set; }

		/// <summary>
		/// Date the activity took place on, or start date for multi-day events
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Optional extra text
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// This is a title for the item
		/// </summary>
		string Name { get; set; }
	}
}