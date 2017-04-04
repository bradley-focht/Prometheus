using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataService.Models
{
	/// <summary>
	/// The input value that a User supplies in response to a User Input that was requested in the Service Request process
	/// </summary>
	public class ServiceRequestUserInput : IServiceRequestUserInput
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// ID of SR that the Input is for
		/// </summary>
		public int ServiceRequestId { get; set; }

		#region Fields
		/// <summary>
		/// ID of Input
		/// </summary>
		public int InputId { get; set; }

		/// <summary>
		/// Type of input on SR
		/// </summary>
		public UserInputType UserInputType { get; set; }

		/// <summary>
		/// Display name for input
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value of the user input
		/// </summary>
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