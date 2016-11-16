using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class LifecycleStatus : ILifecycleStatus
	{
		//PK
		[Key]
		[ForeignKey("Service")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		//FK
		//public int ServiceId { get; set; }

		//TODO: Brad document what the fields in this entity do. Ideally we should have comments in the Model interfaces for ALL fields
		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }
		public int Position { get; set; }

		public bool CatalogVisible { get; set; }
		#endregion
		#region Navigation Properties
		[Required]
		public virtual Service Service { get; set; }
		#endregion
	}
}
