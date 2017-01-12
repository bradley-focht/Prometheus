using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Dto;
using Common.Enums.Entities;

namespace DataService.Models
{
	public class ServiceOption : IServiceOption
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

	    public int Popularity { get; set; }

	    //FK
		public int ServiceId { get; set; }
		public string Features { get; set; }
		public string Benefits { get; set; }
		public string Support { get; set; }
		public string Description { get; set; }
        public string Name { get; set; }
	    public string BusinessValue { get; set; }
		public Guid? Picture { get; set; }
		public string PictureMimeType { get; set; }
		public double PriceUpFront { get; set; }
		public double PriceMonthly { get; set; }
		public double Cost { get; set; }
		public string Usage { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual Service Service { get; set; }
		public int? OptionCategoryId { get; set; }
		#endregion

		public ICollection<ITextInput> TextInputs { get; set; }
		public ICollection<IScriptedSelectionInput> ScriptedSelecentionInputs { get; set; }
		public ICollection<ISelectionInput> SelectionInputs { get; set; }
	}
}
