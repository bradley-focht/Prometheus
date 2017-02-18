﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceRequestOptionTextInput : IServiceRequestOptionTextInput
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int ServiceRequestOptionId { get; set; }
		public int TextInputId { get; set; }
		public string Value { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual TextInput TextInput { get; set; }
		public virtual ServiceRequestOption ServiceRequestOption { get; set; }
	}
}
