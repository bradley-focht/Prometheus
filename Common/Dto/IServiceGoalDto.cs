﻿using System;
using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IServiceGoalDto : IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// ID of Service that the Goal applies to
		/// </summary>
		int ServiceId { get; set; }


		/// <summary>
		/// Extra text for those who like to talk
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Date that the Goal ends
		/// </summary>
		DateTime? EndDate { get; set; }

		/// <summary>
		/// Unique descriptive name
		/// </summary>
		string Name { get; set; }


		/// <summary>
		/// Date that the Goal begins
		/// </summary>
		DateTime? StartDate { get; set; }

		/// <summary>
		/// Identify if short term or long term
		/// </summary>
		ServiceGoalType Type { get; set; }
	}
}