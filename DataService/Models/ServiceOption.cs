using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceOption : IServiceOption
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }



		//FK
		public int ServiceOptionCategoryId { get; set; }

		public int Popularity { get; set; }
		public string Name { get; set; }
		public string BusinessValue { get; set; }
		public Guid? Picture { get; set; }
		public string PictureMimeType { get; set; }
		public double PriceUpFront { get; set; }
		public double PriceMonthly { get; set; }
		public double Cost { get; set; }
		public string Utilization { get; set; }
		public string Included { get; set; }
		public string Procurement { get; set; }
		public string Description { get; set; }
		public string Details { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual ServiceOptionCategory ServiceOptionCategory { get; set; }


		public virtual ICollection<TextInput> TextInputs { get; set; }
		public virtual ICollection<ScriptedSelectionInput> ScriptedSelectionInputs { get; set; }
		public virtual ICollection<SelectionInput> SelectionInputs { get; set; }
		#endregion
	}
}
