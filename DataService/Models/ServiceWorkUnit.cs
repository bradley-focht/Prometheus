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
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public string Name { get; set; }
        public string Department { get; set; }

		public string Contact { get; set; }
		public string Responsibilities { get; set; }
		#endregion
		#region Navigation Propeties
		public virtual Service Service { get; set; }
		#endregion
	}
}
