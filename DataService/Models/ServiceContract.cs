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
		public string Type { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		#endregion
		#region Navigation Properties
		public virtual IService Service { get; set; }
		#endregion
	}
}
