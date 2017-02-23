using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataService.Models
{
	public class ServiceRequestUserInput : IServiceRequestUserInput
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceRequestId { get; set; }

		#region Fields
		public int InputId { get; set; }
		public UserInputType UserInputType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		#region Navigation properties
		public virtual ServiceRequest ServiceRequest { get; set; }
		#endregion
	}
}