using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class LifecycleStatus : ILifecycleStatus
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		//public int ServiceId { get; set; }
		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
		public string Name { get; set; }
        /// <summary>
        /// Used to order the display, this does not have any actual function other than display
        /// </summary>
		public int Position { get; set; }

        /// <summary>
        /// Used to determine whether this should be visible in business/service catalog
        /// </summary>
		public bool CatalogVisible { get; set; }
		#endregion
		#region Navigation Properties

		#endregion
	}
}
