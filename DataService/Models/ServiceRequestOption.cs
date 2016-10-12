using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceRequestOption : IServiceRequestOption
	{
		//PK
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Id { get; set; }
		//FK
		public Guid ServiceId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual Service Service { get; set; }
	}
}
