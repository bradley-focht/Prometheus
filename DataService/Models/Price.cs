using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Entities;

namespace DataService.Models
{
	/// <summary>
	/// Price of a Service Option
	/// </summary>
	public class Price : IPrice
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// Service Option that the Price is for
		/// </summary>
		public int ServiceOptionId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Type of price interval
		/// </summary>
		public PriceType Type { get; set; }

		/// <summary>
		/// Price value in dollars
		/// </summary>
		public decimal Value { get; set; }
		#endregion

		#region Navigation properties
		public virtual ServiceOption ServiceOption { get; set; }
		#endregion
	}
}
