using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceOptionCategoryTag : IServiceOptionCategoryTag
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public int Order { get; set; }
		public int ServiceOptionCategoryId { get; set; }

		public virtual ServiceOptionCategory ServiceOptionCategory { get; set; }
	}
}
