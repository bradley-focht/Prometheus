using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class SwotActivity : ISwotActivity
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }
		public int ServiceSwotId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		#endregion
		#region Navigation Propeties
		public virtual IService Service { get; set; }
		public virtual IServiceSwot ServiceSwot { get; set; }
		#endregion
	}
}
