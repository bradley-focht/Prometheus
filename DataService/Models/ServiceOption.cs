using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataService.Models
{
	public class ServiceOption : IServiceOption
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }
	    public string Description { get; set; }
	    public string BusinessValue { get; set; }
	    public string Picture { get; set; }
	    public ICollection<Tuple<PriceType, double>> Prices { get; set; }
	    public double Cost { get; set; }

	    #region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
