using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceRequestOptionSelectionInput : IServiceRequestOptionSelectionInput
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int ServiceRequestOptionId { get; set; }
		public int SelectionInputId { get; set; }
		public string Value { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual SelectionInput SelectionInput { get; set; }
		public virtual ServiceRequestOption ServiceRequestOption { get; set; }
	}
}