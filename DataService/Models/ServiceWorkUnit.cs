using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceWorkUnit : IServiceWorkUnit
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// ID of Service this Work Unit is assigned to
		/// </summary>
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// The title of a team in the company
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// department the team belongs to
		/// </summary>
		public string Department { get; set; }

		/// <summary>
		/// A manager or someone's name to contact
		/// </summary>
		public string Contact { get; set; }

		/// <summary>
		/// A list of things that this team does to support this service
		/// </summary>
		public string Responsibilities { get; set; }
		#endregion
		#region Navigation Propeties
		public virtual Service Service { get; set; }
		#endregion
	}
}
