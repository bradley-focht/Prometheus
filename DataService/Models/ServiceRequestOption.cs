using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceRequestOption : IServiceRequestOption
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int ServiceOptionId { get; set; }
		public int ServiceRequestId { get; set; }
		public int Quantity { get; set; }

		public int RequestedByUserId { get; set; }
		public int ApproverUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ServiceOption ServiceOption { get; set; }
		public virtual ServiceRequest ServiceRequest { get; set; }
		public virtual ICollection<ServiceRequestOptionScriptedSelectionInput> ServiceRequestOptionScriptedSelectionInputs { get; set; }
		public virtual ICollection<ServiceRequestOptionSelectionInput> ServiceRequestOptionSelectionInputs { get; set; }
		public virtual ICollection<ServiceRequestOptionTextInput> ServiceRequestOptionTextInputs { get; set; }
	}
}
