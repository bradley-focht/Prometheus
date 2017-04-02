using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceContract : IServiceContract
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public string ContractNumber { get; set; }
		public string ServiceProvider { get; set; }
		public string ContractType { get; set; }
		public string Description { get; set; }

		/// <summary>
		/// Date the Contract goes into effect
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Date the Contract Expires
		/// </summary>
		public DateTime ExpiryDate { get; set; }
		#endregion
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
