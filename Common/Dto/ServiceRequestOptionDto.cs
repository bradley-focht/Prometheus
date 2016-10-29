using System;

namespace Common.Dto
{
	public class ServiceRequestOptionDto : IServiceRequestOptionDto
	{
		//PK
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual IServiceDto Service { get; set; }
		#endregion
	}
}
