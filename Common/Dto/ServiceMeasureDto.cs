using System;

namespace Common.Dto
{
	public class ServiceMeasureDto : IServiceMeasureDto
	{
		//TODO: Brad comment
		public int Id { get; set; }
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public string Method { get; set; }
		public string Outcome { get; set; }
		#endregion
	}
}
