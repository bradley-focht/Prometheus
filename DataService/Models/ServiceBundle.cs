using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceBundle : IServiceBundle
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		[Required(ErrorMessage = "Service Bundle: Name is required")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Business Value")]
		public string BusinessValue { get; set; }
		public string Measures { get; set; }
		#endregion
		#region Navigation Properties
		#endregion
	}
}
