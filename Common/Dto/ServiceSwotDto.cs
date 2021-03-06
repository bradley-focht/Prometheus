﻿using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums.Entities;

namespace Common.Dto
{
	public class ServiceSwotDto : IServiceSwotDto
	{
		[HiddenInput]
		public int Id { get; set; }

		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// This is a title of the item
		/// </summary>
		[Display(Order = 1)]
        [Required(ErrorMessage = "Item name is required")]
		public string Item { get; set; }

		/// <summary>
		/// Either strength, weakness, opportunity, threat
		/// </summary>
		[Display(Order = 2)]
        [Required(ErrorMessage = "Item type must be selected")]
		public ServiceSwotType Type { get; set; }

		/// <summary>
		/// Details of items
		/// </summary>
		[AllowHtml]
		[Display(Order = 3)]
		public string Description { get; set; }
		#endregion

		#region Navigation Properties
		/// <summary>
		/// Activities for continuous performance
		/// </summary>
		[Display(Name = "Activities", Order = 4)]
		public ICollection<ISwotActivityDto> SwotActivities { get; set; }
		#endregion
	}
}
